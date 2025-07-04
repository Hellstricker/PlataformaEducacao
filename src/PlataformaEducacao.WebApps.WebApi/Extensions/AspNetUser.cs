﻿using PlataformaEducacao.Core.Interfaces;
using System.Security.Claims;

namespace PlataformaEducacao.WebApps.WebApi.Extensions
{
    public class AspNetUser : IUser
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AspNetUser(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string Name => _contextAccessor.HttpContext!.User.Identity!.Name!;

        public IEnumerable<Claim> GetClaimsIdentity()
        {
            return _contextAccessor.HttpContext!.User.Claims;
        }

        public string GetEmail()
        {
            return IsAuthenticated() ? _contextAccessor.HttpContext!.User.GetEmail() : string.Empty;
        }

        public Guid GetId()
        {
            return IsAuthenticated() ? Guid.Parse(_contextAccessor.HttpContext!.User.GetUserId()) : Guid.Empty;
        }

        public bool IsAuthenticated()
        {
            return _contextAccessor.HttpContext!.User.Identity!.IsAuthenticated;
        }

        public bool IsInRole(string role)
        {
            return _contextAccessor.HttpContext!.User.IsInRole(role);
        }
    }
}
