namespace Externo.Models
{
    public class NovoEmail
    {
        public string Email { get; set; }
        public string? Assunto { get; set; }
        public string? Mensagem { get; set; }

        public NovoEmail() { }

        public NovoEmail(string email, string assunto, string mensagem)
        {
            Email = email;
            Assunto = assunto;
            Mensagem = mensagem;
        }
    }
}
