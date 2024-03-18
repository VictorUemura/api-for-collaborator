using Microsoft.EntityFrameworkCore;

namespace Api_test.Models
{
    public class UsuarioContext : DbContext
    {
        public UsuarioContext(DbContextOptions<UsuarioContext> options) : base(options)
        {
        }
        public DbSet<UsuarioModel> usuarioItems { get; set; } = null!;
    }
}