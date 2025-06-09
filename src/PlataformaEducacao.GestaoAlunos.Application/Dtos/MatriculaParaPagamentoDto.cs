using PlataformaEducacao.GestaoAlunos.Domain;

namespace PlataformaEducacao.GestaoAlunos.Application.Dtos
{
    public class MatriculaParaPagamentoDto
    {
        public Guid Id { get; set; }
        public Guid AlunoId { get; set; }
        public Guid CursoId { get; set; }
        public string? StatusMatricula { get; set; }
    }
}
