namespace Externo.Models
{
    public class Aluguel : NovoAluguel
    {
        public int Bicicleta { get; set; }
        public DateTime HoraInicio { get; set; }
        public int TrancaFim { get; set; }
        public DateTime? HoraFim { get; set; }
        public int Cobranca { get; set; }
    }
}
