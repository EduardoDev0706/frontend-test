using Microsoft.EntityFrameworkCore;
using TesteHelena.Models;

namespace TesteHelena.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<Empresa> Empresas { get; set; } = default!;
    }
}