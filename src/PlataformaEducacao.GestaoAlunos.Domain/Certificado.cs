using PlataformaEducacao.Core.DomainObjects;

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
            Validar();
        }

        //TODO:Adicionar validação nesta entidade e nome do curso na entitade matriula

        public void Validar()
        {
            Validacoes.ValidarSeVazio(NomeCurso, "O nome do Curdo não pode ser vazio");
            Validacoes.ValidarSeDiferente(MatriculaId, Guid.Empty, "O Id da matrícula não pode ser vazio");
        }

    }
}
