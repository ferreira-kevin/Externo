using Externo.Models;
using Microsoft.EntityFrameworkCore;

namespace Externo.Infrastructure
{
    public class ExternoDbContext : DbContext
    {
        public ExternoDbContext(DbContextOptions<ExternoDbContext> options) : base(options) { }

        public DbSet<Email> Emails { get; set; }
        public DbSet<Cobranca> Cobrancas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
