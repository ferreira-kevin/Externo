using Externo.Infrastructure;
using Externo.Models;

namespace Externo.Repositories
{
    public interface IEmailRepository
    {
        Email Create(NovoEmail email);
    }

    public class EmailRepository : IEmailRepository
    {
        private readonly ExternoDbContext _dbContext;
        public EmailRepository(ExternoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Email Create(NovoEmail novoEmail)
        {
            var email = new Email(novoEmail);
            _dbContext.Emails.Add(email);
            _dbContext.SaveChanges();
            return email;
        }
    }
}
