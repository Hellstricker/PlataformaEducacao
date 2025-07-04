using Microsoft.AspNetCore.Identity;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace PlataformaEducacao.WebApps.WebApi.Extensions.Identity
{
    public class CustomLookupNormalizer : ILookupNormalizer
    {
        public string? Normalize(string? key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return null;

            string semAcentos = RemoverAcentos(key);
            return semAcentos.ToUpperInvariant();
        }

        private static string RemoverAcentos(string texto) =>
            new string(texto.Normalize(NormalizationForm.FormD)
                .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
                .ToArray());

        [return: NotNullIfNotNull("name")]
        public string? NormalizeName(string? name) =>
        Normalize(name);


        [return: NotNullIfNotNull("email")]
        public string? NormalizeEmail(string? email) =>
        Normalize(email);

    }

}
