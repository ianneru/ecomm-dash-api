using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.EntityConfigurations
{
    public class EquipeEntityTypeConfiguration
        : IEntityTypeConfiguration<Equipe>
    {
        public void Configure(EntityTypeBuilder<Equipe> builder)
        {
            builder.ToTable("Equipes");

            builder.HasKey(ci => ci.Id);

            builder.Property(prop => prop.Nome)
                .HasColumnType("nvarchar(255)");

            builder.Property(prop => prop.Descricao)
                .HasColumnType("nvarchar(255)");
        }
    }
}
