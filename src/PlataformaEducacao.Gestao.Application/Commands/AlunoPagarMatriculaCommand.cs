using PlataformaEducacao.Core.Messages;
using PlataformaEducacao.Gestao.Application.Commands.Validations;
using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Gestao.Application.Commands
{
    public class AlunoPagarMatriculaCommand:Command
    {
        public Guid AlunoId { get; private set; }
        public Guid CursoId { get; private set; }        
        public string NomeCartao { get; private set; }        
        public string NumeroCartao { get; private set; }        
        public string MesAnoExpiracao { get; private set; }        
        public string Ccv { get; private set; }

        public AlunoPagarMatriculaCommand(Guid alunoId, Guid cursoId, string nomeCartao, string numeroCartao, string mesAnoExpiracao, string ccv) 
        {
            AlunoId = alunoId;
            CursoId = cursoId;
            NomeCartao = nomeCartao;
            NumeroCartao = numeroCartao;
            MesAnoExpiracao = mesAnoExpiracao;
            Ccv = ccv;
        }

        public override bool EhValido()
        {
            ValidationResult = new AlunoPagarMatriculaCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
