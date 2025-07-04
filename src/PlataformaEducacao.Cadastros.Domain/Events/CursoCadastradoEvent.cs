using PlataformaEducacao.Core.Messages;

namespace PlataformaEducacao.Cadastros.Domain.Events
{
    public class CursoCadastradoEvent: Event
    {        
        public string Titulo { get; private set; }
        public decimal Valor { get; private set; }
        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }

        public CursoCadastradoEvent(string titulo, decimal valor, string descricao, int cargaHoraria)
        {
            Titulo = titulo;
            Valor = valor;
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
        }
    }
}
