namespace Externo.Infrastructure
{
    public interface IFila
    {
        public void Enfileirar(string mensagem);
        public string Desenfileirar();
        public bool AindaHaMensagens();
    }

    public class Fila : IFila
    {
        private readonly Queue<string> _queue;

        public Fila()
        {
            _queue = new Queue<string>();
        }

        public void Enfileirar(string message)
        {
            _queue.Enqueue(message);
        }

        public string Desenfileirar()
        {
            return _queue.Dequeue();
        }

        public bool AindaHaMensagens()
        {
            return _queue.Any();
        }
    }
}
