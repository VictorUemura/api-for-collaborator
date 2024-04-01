using Microsoft.EntityFrameworkCore;

namespace Api_test.Models
{
    public class ColaboradorContext : DbContext
    {
        public ColaboradorContext(DbContextOptions<ColaboradorContext> options) : base(options)
        {
        }
        public DbSet<ColaboradorModel> ColaboradorItems { get; set; } = null!;
    }
}