using Externo.Infrastructure;
using Externo.Models;

namespace Externo.Repositories
{
    public interface ICobrancaRepository
    {
        public Cobranca Create(NovaCobranca novaCobranca);
        public Cobranca Find(int id);
    }

    public class CobrancaRepository : ICobrancaRepository
    {
        private readonly ExternoDbContext _dbContext;
        public CobrancaRepository(ExternoDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Cobranca Create(NovaCobranca novaCobranca)
        {
            var cobranca = new Cobranca(novaCobranca);
            _dbContext.Cobrancas.Add(cobranca);
            _dbContext.SaveChanges();
            return cobranca;
        }

        public Cobranca Find(int id)
        {
            return _dbContext.Cobrancas.First(c => c.Id == id);
        }
    }
}
