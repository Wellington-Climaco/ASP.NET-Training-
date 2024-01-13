using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TreinandoApi.Models;

namespace TreinandoApi.Data.Mapeamento
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd().UseIdentityColumn();
           
            builder.Property(x => x.Nome).IsRequired().HasColumnName("Nome").HasColumnType("VARCHAR").HasMaxLength(80);

            builder.Property(x => x.email).IsRequired().HasColumnName("Email").HasColumnType("VARCHAR").HasMaxLength(160);

            builder.Property(x => x.password).IsRequired().HasColumnName("Password").HasColumnType("VARCHAR").HasMaxLength(80);

            builder.HasIndex(x => x.email).IsUnique();


        }
    }
}
