using Externo.Infrastructure;
using Externo.Models;
using Externo.Repositories;
using System.Net.Mail;

namespace Externo.UseCases
{
    public interface IEnviarEmailUseCase
    {
        public Email Enviar(NovoEmail email);
    }

    public class EnviarEmailUseCase : IEnviarEmailUseCase
    {
        private readonly IEmailSender _emailSender;
        private readonly IEmailRepository _emailRepository;

        public EnviarEmailUseCase(IEmailSender emailSender, IEmailRepository emailRepository)
        {
            _emailSender = emailSender;
            _emailRepository = emailRepository;
        }

        public Email Enviar(NovoEmail email)
        {
            if (EmailValido(email))
            {
                _emailSender.Send(email);
                return _emailRepository.Create(email);
            }
            else
            {
                throw new Exception("Não foi possível enviar E-mail.");
            }
        }

        static bool EmailValido(NovoEmail email)
        {
            try
            {
                var mailAddress = new MailAddress(email.Email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
