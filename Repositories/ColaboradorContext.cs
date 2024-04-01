using Microsoft.EntityFrameworkCore;
using Api_test.Models;

namespace Api_test.Repositories
{
    public class ColaboradorContext : DbContext
    {
        public ColaboradorContext(DbContextOptions<ColaboradorContext> options) : base(options)
        {
        }
        public DbSet<ColaboradorModel> ColaboradorItems { get; set; } = null!;
    }
}