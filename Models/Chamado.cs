namespace IntegracaoChamados.Models
{
    public class Chamado
    {
        public int ID { get; set; }
        public string TITULO { get; set; }
        public string AREA { get; set; }
        public string SITUACAO { get; set; }
        public DateTime DATAABERTURA { get; set; }
    }
}
