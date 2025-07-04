using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PlataformaEducacao.Core.Communications.Mediators;
using PlataformaEducacao.Core.Interfaces;
using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Core.Messages.Notifications;
using PlataformaEducacao.WebApps.WebApi.Contexts;
using PlataformaEducacao.WebApps.WebApi.Extensions;
using PlataformaEducacao.WebApps.WebApi.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PlataformaEducacao.WebApps.WebApi.Controllers
{
    public abstract class MainSignInController : BaseController
    {
        protected readonly UserManager<IdentityUser<Guid>> _userManager;
        protected readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly JwtSettings _jwtSettings;
        private readonly ApplicationDbContext _context;
        public MainSignInController(
            IDomainNotificationHandler notifications, 
            IMediatorHandler mediatorHandler, 
            IUser loggedUser,
            UserManager<IdentityUser<Guid>> userManager,
            RoleManager<IdentityRole<Guid>> roleManager,
            IOptions<JwtSettings> jwtSettings,
            ApplicationDbContext context)
            : base(notifications, mediatorHandler, loggedUser)
        {    
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
            _context = context;
        }

        protected async Task<LoginResponseViewModel> GetJwt(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user!);
            var claims = await _userManager.GetClaimsAsync(user!);
            var usuario = await _context.Users.FirstOrDefaultAsync(p => p.Id == user!.Id);

            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user!.Id.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user!.Email!));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, ToUnixEpocheDate(DateTime.UtcNow).ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpocheDate(DateTime.Now).ToString(), ClaimValueTypes.Integer64));
            claims.Add(new Claim("nome", usuario?.UserName!));

            foreach (var role in roles)
                claims.Add(new Claim("role", role));


            var identityClaims = new ClaimsIdentity(claims);


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo!);



            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor()
            {
                Subject = identityClaims,
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.HorasParaExpirar),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            return new LoginResponseViewModel()
            {
                AccessToken = tokenHandler.WriteToken(token),
                ExpiresIn = TimeSpan.FromHours(_jwtSettings.HorasParaExpirar).TotalSeconds,
                UserToken = new UserTokenViewModel()
                {
                    Id = user.Id.ToString(),
                    Email = user.Email,
                    Claims = claims.Select(c => new ClaimViewModel() { Type = c.Type, Value = c.Value })
                }
            };


        }

        private static long ToUnixEpocheDate(DateTime date)
        {
            return (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        }
    }    
}
