using Externo.Models;
using System.Net.Mail;

namespace Externo.Infrastructure
{
    public interface IEmailSender
    {
        void Send(NovoEmail email);
    }

    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;
        private readonly string _remetente;

        public EmailSender(SmtpClient smtpClient, IConfiguration configuration)
        {
            _smtpClient = smtpClient;
            _configuration = configuration;

            var remetente = _configuration.GetValue<string>("SmtpSettings:Mailer");
            if (string.IsNullOrEmpty(remetente))
                throw new Exception("Remetente não configurado.");
            _remetente = remetente;
        }

        public void Send(NovoEmail email)
        {
            try
            {
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_remetente),
                    Subject = email.Assunto,
                    Body = email.Mensagem,
                    IsBodyHtml = true,
                };
                mailMessage.To.Add(email.Email);

                _smtpClient.Send(mailMessage);
            }
            catch
            {
                throw new Exception("Não foi possível enviar E-mail.");
            }
        }
    }
}
