namespace PlataformaEducacao.GestaoCursos.Application.Dtos
{
    public class CursoDto
    {
        public Guid Id { get; set; }
        public required string Nome { get; set; }
        public required string Conteudo { get; set; }
        public required string Objetivo { get; set; }
        public required decimal Valor { get; set; }
        public IEnumerable<AulaDto>? Aulas { get; set; }
    }
}
