namespace IntegracaoChamados.Models
{
    public class Chamado
    {
        public int NUM_CHAMADO { get; set; }
        public string TITULO { get; set; }
        public string DESCRICAO { get; set; }
        public string SISTEMA { get; set; }
        public string SITUACAO { get; set; }
        public DateTime DATA_SOLICITACAO { get; set; }
        public DateTime DATA_SLA { get; set; }
    }
}
