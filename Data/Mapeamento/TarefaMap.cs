using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreinandoApi.Models;

namespace TreinandoApi.Data.Mapeamento
{
    public class TarefaMap : IEntityTypeConfiguration<Tarefa1>
    {
        public void Configure(EntityTypeBuilder<Tarefa1> builder)
        {
            builder.ToTable("Tarefa");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd().UseIdentityColumn();

            builder.Property(x => x.NomeTarefa).IsRequired().HasColumnName("NomeTarefa").HasColumnType("VARCHAR").HasMaxLength(80);

            builder.Property(x => x.Descricao).IsRequired().HasColumnName("Descricao").HasColumnType("TEXT");

            builder.Property(x => x.DataCriacao).IsRequired().HasColumnName("DataCriacao").HasColumnType("SMALLDATETIME").HasDefaultValueSql("GETDATE()");

            builder.HasOne(x => x.Usuario).WithMany(x => x.ListaTarefas).HasConstraintName("FK_Tarefa_Usuario").OnDelete(DeleteBehavior.Cascade);
        }
    }
}