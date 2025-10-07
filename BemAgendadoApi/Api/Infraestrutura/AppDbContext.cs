using Microsoft.EntityFrameworkCore;
using Api.Model;
using Api.Models;


namespace Api.Infraestrutura
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AgendamentoModels> agenda { get; set; }

        public DbSet<UsuariosModels> Usuarios { get; set; }
        public DbSet<PacienteModels> paciente { get; set; }

    }

}

