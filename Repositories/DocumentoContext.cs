using Microsoft.EntityFrameworkCore;
using Api_test.Models;

namespace Api_test.Repositories
{
    public class DocumentoContext : DbContext
    {
        public DocumentoContext(DbContextOptions<DocumentoContext> options) : base(options)
        {
        }
        public DbSet<DocumentoModel> DocumentoItems { get; set; } = null!;
    }
}