namespace PlataformaEducacao.Pagamentos.Business
{
    public class DadosCartao
    {
        public string NomeCartao { get; set; }
        public string NumeroCartao { get; set; }
        public string MesAnoExpiracao { get; set; }
        public string Ccv { get; set; }

        public override string ToString()
        {
            return $"{NomeCartao} - {NumeroCartao} - {MesAnoExpiracao} - {Ccv}";
        }
    }
}