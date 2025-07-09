namespace PlataformaEducacao.Core.Messages.IntegrationEvents
{
    public class AlunoPagouMatriculaEvent:IntegrationEvent
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public string NomeCartao { get; private set; }
        public string NumeroCartao { get; private set; }
        public string MesAnoExpiracao { get; private set; }
        public string Ccv { get; private set; }
        public decimal Valor { get; private set; }


        public AlunoPagouMatriculaEvent(Guid alunoId, Guid cursoId, decimal valor, string nomeCartao, string numeroCartao, string mesAnoExpiracao, string ccv)
        {
            AggregateId = alunoId;
            AlunoId = alunoId;
            CursoId = cursoId;
            Valor = valor;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            MesAnoExpiracao = mesAnoExpiracao;
            Ccv = ccv;
        }

    }
}
