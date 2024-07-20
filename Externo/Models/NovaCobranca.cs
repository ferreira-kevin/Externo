namespace Externo.Models
{
    public class NovaCobranca
    {
        public decimal Valor { get; set; }
        public int Ciclista { get; set; }

        public NovaCobranca () { }

        public NovaCobranca (decimal valor, int ciclista)
        {
            Valor = valor;
            Ciclista = ciclista;
        }
    }
}
