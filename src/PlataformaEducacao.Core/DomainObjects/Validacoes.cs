namespace PlataformaEducacao.Core.DomainObjects
{
    public static class Validacoes
    {
        public static void ValidarSeVazio(string? valor, string mensagem)
        {
            if (string.IsNullOrEmpty(valor))
                throw new DomainException(mensagem);
        }

        public static void ValidarSeDiferente(object object1, object object2, string mensagem)
        {
            if (object1.Equals(object2))
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarSeMenorOuIgual(decimal valor, decimal comparador, string mensagem)
        {
            if (valor <= comparador)
            {
                throw new DomainException(mensagem);
            }
        }
    }
}