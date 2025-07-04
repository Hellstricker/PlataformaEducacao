using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class MatricularAlunoCommand : Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }
        public string? CursoNome { get; private set; }
        public decimal CursoValor { get; private set; }
        public int CursoTotalAulas { get; private set; }

        public MatricularAlunoCommand(Guid alunoId, Guid cursoId, string? cursoNome, decimal cursoValor, int cursoTotalAulas)
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            CursoNome = cursoNome;
            CursoValor = cursoValor;
            CursoTotalAulas = cursoTotalAulas;
        }

        public override bool EhValido()
        {
            ValidationResult = new MatricularAlunoCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
