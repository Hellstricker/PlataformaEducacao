namespace PlataformaEducacao.Gestao.Application.Queries.ViewModels
{
    public class MatriculasAlunoViewModel
    {
        public string Nome { get; set; }
        public List<MatriculaViewModel> Matriculas { get; set; } = new List<MatriculaViewModel>();
    }

    public class MatriculaViewModel
    {
        public string Curso { get; set; }
    }
}
