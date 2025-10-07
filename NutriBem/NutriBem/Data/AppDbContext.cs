using Microsoft.EntityFrameworkCore;
using NutriBem.Models;

namespace NutriBem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
       
        
        public DbSet<PacienteModels> Usuarios { get; set; }
    
    }
    
}
