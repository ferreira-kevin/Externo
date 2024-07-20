using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Externo.Models
{
    public class Email : NovoEmail
    {
        public int Id { get; set; }

        public Email() { }

        public Email(NovoEmail novoEmail) : base(novoEmail.Email, novoEmail.Assunto, novoEmail.Mensagem)
        {
        }
    }
}
