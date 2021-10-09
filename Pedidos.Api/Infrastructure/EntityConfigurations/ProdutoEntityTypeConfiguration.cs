using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.EntityConfigurations
{
    public class ProdutoEntityTypeConfiguration
        : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.Nome)
                .HasColumnType("nvarchar(255)");

            builder.Property(cb => cb.Descricao)
                .HasColumnType("nvarchar(255)");

            builder.Property(cb => cb.Valor)
                .HasColumnType("decimal(5,2)");
        }
    }
}
