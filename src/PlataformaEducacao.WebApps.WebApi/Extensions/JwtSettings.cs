﻿namespace PlataformaEducacao.WebApps.WebApi.Extensions
{
    public class JwtSettings
    {
        public string? Segredo { get; set; }
        public int HorasParaExpirar { get; set; }
        public string? Emissor { get; set; }
        public string? Audiencia { get; set; }
    }
}
