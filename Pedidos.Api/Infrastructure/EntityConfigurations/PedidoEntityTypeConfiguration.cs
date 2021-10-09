using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.EntityConfigurations
{
    public class PedidoEntityTypeConfiguration
        : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(ci => ci.Id);

            builder.Property(cb => cb.DataCriacao)
                .IsRequired();
        }
    }
}
