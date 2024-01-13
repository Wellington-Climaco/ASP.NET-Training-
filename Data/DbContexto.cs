using Microsoft.EntityFrameworkCore;
using TreinandoApi.Data.Mapeamento;
using TreinandoApi.Models;

namespace TreinandoApi.Data
{
    public class DbContexto : DbContext
    {
        public DbSet<Tarefa>  Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost,1433;Database=DadosTarefa;User ID=sa;Password=1q2w3e4r@#$;TrustServerCertificate=True");           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

    }
}
