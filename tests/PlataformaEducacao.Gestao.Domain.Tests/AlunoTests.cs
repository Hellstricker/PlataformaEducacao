using Moq;
using Moq.AutoMock;
using PlataformaEducacao.Core.DomainObjects;
using PlataformaEducacao.Gestao.Domain.Tests.Configs;
using PlataformaEducacao.Gestao.Domain.Validations;

namespace PlataformaEducacao.Gestao.Domain.Tests
{
    [Collection(nameof(GestaoDomainCollection))]
    public class AlunoTests
    {

        private readonly GestaoDomainTestsFixture _fixtures;
        private readonly AutoMocker _mocker;

        public AlunoTests(GestaoDomainTestsFixture fixtures)
        {
            _fixtures = fixtures;
            _mocker = new AutoMocker();
        }

        [Fact(DisplayName = "Aluno Valido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_EntidadeValida_DevePassarNaValidacao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            // Act
            var result = aluno.EhValido();
            // Assert
            Assert.True(result, "O aluno deve ser considerado válido.");
            Assert.Empty(aluno.ValidationResult.Errors);
        }

        [Fact(DisplayName = "Aluno Inválido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_EntidadeInValida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoInvaValido();
            // Act
            var result = aluno.EhValido();
            // Assert
            Assert.False(result, "O aluno deve ser considerado inválido.");
            Assert.NotEmpty(aluno.ValidationResult.Errors);
            Assert.Equal(2, aluno.ValidationResult.Errors.Count);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.NomeTamanhoMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.EmailFormatoMensagemErro);
        }

        [Fact(DisplayName = "Aluno Inválido Não Preenchido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_EntidadeNaoPreenchida_NaoDevePassarNaValidacao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoInvalidoDadosEmBranco();
            // Act
            var result = aluno.EhValido();
            // Assert
            Assert.False(result, "O aluno deve ser considerado inválido.");
            Assert.NotEmpty(aluno.ValidationResult.Errors);
            Assert.Equal(4, aluno.ValidationResult.Errors.Count);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.NomeVazioMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.NomeTamanhoMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.EmailVazioMensagemErro);
            Assert.Contains(aluno.ValidationResult.Errors, e => e.ErrorMessage == AlunoValidation.EmailFormatoMensagemErro);
        }

        [Fact(DisplayName = "Aluno Matricular Com Sucesso")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_RealizarMatriculaValida_NaoDeveRetornarErros()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            
            // Act
            aluno.Matricular(matricula);

            // Assert
            Assert.Contains(aluno.Matriculas, m => m.Id == matricula.Id);
        }

        [Fact(DisplayName = "Aluno Realizar Matricula Mesmo Curso")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_RealizarMatriculaMesmoCurso_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);

            // Act
            var ex = Assert.Throws<DomainException>(() => aluno.Matricular(matricula));

            // Assert            
            Assert.Contains(Aluno.AlunoJaMatriculado, ex.Message);
        }

        [Fact(DisplayName = "Aluno Pagar Matricula Status Invalido")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_PagarMatriculaStatusInvalido_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaComStatusInvalido(aluno.Id);
            aluno.Matricular(matricula);

            // Act
            var ex = Assert.Throws<DomainException>(() => aluno.PagarMatricula(matricula.Curso.CursoId));

            // Assert            
            Assert.Contains(Matricula.MatriculaNaoEstaPendente, ex.Message);
        }

        [Fact(DisplayName = "Aluno Pagar Matricula Status Correto")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_PagarMatriculaStatusCorreto_NaoDeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);            

            // Act
             aluno.PagarMatricula(matricula.Curso.CursoId);

            //Assert
            Assert.Equal(StatusMatriculaEnum.EM_ANDAMENTO, matricula.Status);
        }

        [Fact(DisplayName = "Aluno FinalizarAula Aula em Curso Não Em Andamento")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_FinalizarAulaCursoNaoAndamento_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);

            // Act
            var ex = Assert.Throws<DomainException>(() => aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid()));

            //Assert
            Assert.Contains(Matricula.MatriculaNaoEstaEmAndamento, ex.Message);
        }

        [Fact(DisplayName = "Aluno FinalizarAula Aula em Curso Não Matriculado")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_FinalizarAulaAulaCursoNaoMatriculado_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);

            // Act
            var ex = Assert.Throws<DomainException>(() => aluno.FinalizarAula(Guid.NewGuid(), Guid.NewGuid()));

            //Assert
            Assert.Contains(Aluno.AlunoNaoEstaMatriculado , ex.Message);
        }

        [Fact(DisplayName = "Aluno FinalizarAula Aula com Sucesso")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_FinalizarAulaComSucesso_NaoDeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);
            aluno.PagarMatricula(matricula.Curso.CursoId);
            var aulaId= Guid.NewGuid();
            // Act
            aluno.FinalizarAula(matricula.Curso.CursoId, aulaId);

            //Assert            
            Assert.NotNull(aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado);
            Assert.True(aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado!.Progresso > 0);
            Assert.Single(aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado!.AulasFinalizadas.Where(a => a.AulaId == aulaId));
        }

        [Fact(DisplayName = "Aluno FinalizarAula Em Duplicidade")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Aluno_FinalizarAulaEmDuplicidade_NaoDeveAlterarEstado()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValida(aluno.Id);
            aluno.Matricular(matricula);
            aluno.PagarMatricula(matricula.Curso.CursoId);
            var aulaId = Guid.NewGuid();
            aluno.FinalizarAula(matricula.Curso.CursoId, aulaId);
            var progresso = aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado!.Progresso;
            var quantidadeAulasFinalizadas = aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado!.AulasFinalizadas.Where(a => a.AulaId == aulaId);


            // Act
            aluno.FinalizarAula(matricula.Curso.CursoId, aulaId);

            //Assert            
            Assert.NotNull(aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado);
            Assert.Equal(progresso, aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado!.Progresso);
            Assert.Equal(quantidadeAulasFinalizadas, aluno.ObterMatricula(matricula.Curso.CursoId).Curso.HistoricoAprendizado!.AulasFinalizadas.Where(a => a.AulaId == aulaId));
        }

        [Fact(DisplayName = "Aluno Finalizar Matricula Curso Inexistente")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Matricula_FinalizarMatriculaCursoInexistente_DeveGerarExcecao()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaDuasAulas(aluno.Id);
            aluno.Matricular(matricula);
            // Act
            var ex = Assert.Throws<DomainException>(() => aluno.FinalizarMatricula(Guid.NewGuid()));
            
            // Assert            
            Assert.Equal(Aluno.AlunoNaoEstaMatriculado, ex.Message);
        }


        [Fact(DisplayName = $"Aluno Finalizar Matricula Status {nameof(StatusMatriculaEnum.PENDENTE_PAGAMENTO)}")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Matricula_FinalizarMatriculaPendenteDePagamento_MatriculaDeveContinuarPendenteDePagamento()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaDuasAulas(aluno.Id);
            aluno.Matricular(matricula);
            // Act
            aluno.FinalizarMatricula(matricula.Curso.CursoId);

            // Assert            
            Assert.Equal(StatusMatriculaEnum.PENDENTE_PAGAMENTO, matricula.Status);
        }

        [Fact(DisplayName = "Aluno Finzalizar Matricula Progresso Sem progresso completo")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Matricula_FinalizarMatriculaSemProgressoSemCompleto_MatriculaDeveContinuarEmAndamento()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaDuasAulas(aluno.Id);
            aluno.Matricular(matricula);
            aluno.PagarMatricula(matricula.Curso.CursoId);            
            aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid());

            // Act
            aluno.FinalizarMatricula(matricula.Curso.CursoId);

            // Assert            
            Assert.Equal(StatusMatriculaEnum.EM_ANDAMENTO, matricula.Status);
        }

        [Fact(DisplayName = "Aluno Finzalizar Matricula Progresso Completo")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Matricula_FinalizarMatriculaSemProgressoCompleto_MatriculaDeveContinuarEmAndamento()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaDuasAulas(aluno.Id);
            aluno.Matricular(matricula);
            aluno.PagarMatricula(matricula.Curso.CursoId);            
            aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid());
            aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid());

            // Act
            aluno.FinalizarMatricula(matricula.Curso.CursoId);

            // Assert            
            Assert.Equal(StatusMatriculaEnum.CONCLUIDA, matricula.Status);
        }


        [Fact(DisplayName = "Aluno Gerar Certificado de Curso Incompleto")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Matricula_GerarCertificadoDeCursoIncompleto_CertificadoNaoDeveSerGerado()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaDuasAulas(aluno.Id);
            aluno.Matricular(matricula);
            aluno.PagarMatricula(matricula.Curso.CursoId);
            aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid());


            // Act
            aluno.GerarCertificado(matricula.Curso.CursoId);

            // Assert            
            Assert.Empty(aluno.Certificados);
        }

        [Fact(DisplayName = "Aluno Gerar Certificado de Curso Finalizado")]
        [Trait("Categoria", "Domínio Gestao Entidade Aluno")]
        public void Matricula_GerarCertificadoDeCursoFinalizado_CertificadoDeveSerGerado()
        {
            // Arrange
            var aluno = _fixtures.GerarAlunoValido();
            var matricula = _fixtures.GerarMatriculaValidaDuasAulas(aluno.Id);
            aluno.Matricular(matricula);
            aluno.PagarMatricula(matricula.Curso.CursoId);
            aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid());
            aluno.FinalizarAula(matricula.Curso.CursoId, Guid.NewGuid());
            aluno.FinalizarMatricula(matricula.Curso.CursoId);

            // Act
            aluno.GerarCertificado(matricula.Curso.CursoId);

            // Assert            
            Assert.NotEmpty(aluno.Certificados);
            Assert.Contains(matricula.Curso.CursoNome, aluno.Certificados.Select(c=>c.NomeCurso));
        }
    }
}
