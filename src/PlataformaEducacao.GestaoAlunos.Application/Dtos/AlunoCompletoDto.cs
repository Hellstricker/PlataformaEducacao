namespace PlataformaEducacao.GestaoAlunos.Application.Dtos
{
    public class AlunoCompletoDto
    {
        public required Guid Id { get; set; }        
        public required string Nome { get; set; }
        public IEnumerable<MatriculaDto>? Matriculas { get; set; }
    }

    public class MatriculaDto
    {
        public required Guid Id { get; set; }
        public required Guid CursoId { get; set; }
        public required string NomeCurso { get; set; }               
        public required string StatusMatricula { get; set; }
        public decimal Progresso { get; set; }
        public CertificadoDto? Certificado { get; set; }

        public IEnumerable<AulaFinalizadaDto>? AulasFinalizadas { get; set; }

    }

    public class AulaFinalizadaDto
    {
        public required Guid Id { get; set; }
        public required Guid AulaId { get; set; }
        public required DateTime DataFinalizacao { get; set; }
    }

    public class CertificadoDto
    {
        public required Guid Id { get; set; }
        public required Guid NumeroCertificado { get; set; }
        public required string NomeCurso { get; set; }
        public required DateTime DataConclusao { get; set; }
        public required bool Ativo { get; set; }
    }
}
