using System.Globalization;
using System.Text;

namespace PlataformaEducacao.WebApps.WebApi.Extensions.String
{
    public static class StringExtensions
    {
        public static string NormalizedToIdentityUserName(this string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return texto;

            var normalizado = texto.Normalize(NormalizationForm.FormD);
            var sb = new StringBuilder();

            foreach (var c in normalizado)
            {
                var categoria = CharUnicodeInfo.GetUnicodeCategory(c);
                if (categoria != UnicodeCategory.NonSpacingMark)
                    sb.Append(c);
            }

            return sb.ToString().Replace(" ", ".").ToLowerInvariant().Normalize(NormalizationForm.FormC);
        }
    }

}
