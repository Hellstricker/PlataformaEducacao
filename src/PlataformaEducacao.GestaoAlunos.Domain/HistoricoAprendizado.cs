using PlataformaEducacao.Core.DomainObjects;

namespace PlataformaEducacao.GestaoAlunos.Domain
{
    public class HistoricoAprendizado
    {        
        public decimal? Progresso { get; private set; }

        public bool EstaCompleto() => Progresso == 100;

        public void AtualizarProgresso(int aulasAssistidas, int totalAulas)
        {
            if (totalAulas <= 0) throw new DomainException("Total de aulas deve ser maior que zero.");
            if (aulasAssistidas < 0 || aulasAssistidas > totalAulas) throw new ArgumentOutOfRangeException(nameof(aulasAssistidas), "Número de aulas assistidas inválido.");            
            Progresso = (decimal)aulasAssistidas / totalAulas * 100;
        }
    }
}
