namespace PlataformaEducacao.Cadastros.Domain
{
    public class ConteudoProgramatico
    {
        public const int MINIMO_CARACTERES_DESCRICAO = 10;
        public const int MAXIMO_CARACTERES_DESCRICAO = 500;

        public const string DescricaoSemMinimoCaracteres = "A descricao do conteúdo programático deve ter no mínimo 10 caracteres.";
        public const string DescricaoMaiorMaximoCaracteres = "A descricao do conteúdo programático deve ter no máximo 500 caracteres.";
        public const string DescricaoEmBrancoOuNulo = "A descrição do conteúdo programático não pode ser em branco ou nulo.";
        public const string CargaHorariaMenorOuIgualAZero = "A carga horária do conteúdo programático não pode ser menor ou igual a zero.";

        public string Descricao { get; private set; }
        public int CargaHoraria { get; private set; }

        private ConteudoProgramatico() { }

        public ConteudoProgramatico(string descricao, int cargaHoraria)
        {
            Descricao = descricao;
            CargaHoraria = cargaHoraria;
        }
    }    
}
