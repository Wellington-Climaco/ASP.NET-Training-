using Microsoft.EntityFrameworkCore;

using TreinandoApi.Data.Mapeamento;
using TreinandoApi.Models;

namespace TreinandoApi.Data
{
    public class DbContexto : DbContext
    {
        public DbSet<Tarefa1>  Tarefas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        
        public DbContexto(DbContextOptions<DbContexto> options)
            : base(options)
        {           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TarefaMap());
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

    }
}
