namespace PlataformaEducacao.Core.DomainObjects.Dtos
{
    public class PagamentoMatriculaDto
    {        
        public Guid CursoId { get; set; }
        public Guid AlunoId { get; set; }
        public decimal Valor { get; set; }
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string MesAnoExpiracao { get; set; }
        public string Ccv { get; set; }

        public PagamentoMatriculaDto(Guid cursoId, Guid alunoId, decimal valor, string nomeCartao, string numeroCartao, string mesAnoExpiracao, string ccv)
        {            
            CursoId = cursoId;
            AlunoId = alunoId;
            Valor = valor;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            MesAnoExpiracao = mesAnoExpiracao;
            Ccv = ccv;
        }
    }
}
