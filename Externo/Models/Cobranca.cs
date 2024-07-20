namespace Externo.Models
{
    public class Cobranca : NovaCobranca
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public DateTime HoraSolicitacao { get; set; }
        public DateTime? HoraFinalizacao { get; set; }

        public Cobranca () { }

        public Cobranca (NovaCobranca novaCobranca) : base (novaCobranca.Valor, novaCobranca.Ciclista) { }
    }
}
