using Microsoft.EntityFrameworkCore;
using Api_test.Models;

namespace Api_test.Repositories
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<ColaboradorModel> Colaboradores { get; set; }
        public DbSet<DocumentoModel> Documentos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
