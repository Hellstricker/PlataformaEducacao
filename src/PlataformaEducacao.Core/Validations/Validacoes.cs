
using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.Core.Validations
{
    public static class Validacoes
    {
        public static void ValidarSeVazio(string? valor, string mensagem)
        {
            if (string.IsNullOrEmpty(valor))
                throw new DomainException(mensagem);
        }

        public static void ValidarSeMenorOuIgual(decimal valor, decimal comparador, string mensagem)
        {
            if (valor <= comparador)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarSeNulo(object objeto, string mensagem)
        {
            if(objeto == null)
            {
                throw new DomainException(mensagem);
            }
        }

        public static void ValidarSeStringEntreCaracteres(string valor, int minimo, int maximo, string mensagemMinimo, string mensagemMaximo)
        {
            if (valor.Length < minimo)
            {
                throw new DomainException(mensagemMinimo);
            }
            if (valor.Length > maximo)
            {
                throw new DomainException(mensagemMaximo);
            }
        }
    }
}
