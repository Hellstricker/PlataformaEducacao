using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.GestaoAlunos.Domain.Validations;

namespace PlataformaEducacao.GestaoAlunos.Domain
{
    public class Certificado : Entity
    {        
        public Guid NumeroCertificado { get; private set; }
        public DateTime DataCadastro { get; private set; }
        public string NomeCurso { get; private set; }        
        public bool Ativo { get; private set; }
        public Guid MatriculaId { get; private set; }

        public Matricula Matricula { get; private set; }

        public Certificado(string nomeCurso, Guid matriculaId)
        {   
            NumeroCertificado = Guid.NewGuid();            
            NomeCurso = nomeCurso;                        
            Ativo = true;
            MatriculaId = matriculaId;         
        }
       
        public override bool EhValido()
        {
            ValidationResult = new CertificadoValidation().Validate(this);
            return ValidationResult.IsValid;
        }

    }
}
