using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.EntityConfigurations
{
    public class EncomendaEntityTypeConfiguration
        : IEntityTypeConfiguration<Encomenda>
    {
        public void Configure(EntityTypeBuilder<Encomenda> builder)
        {
            builder.ToTable("Encomendas");

            builder.HasKey(ci => ci.Id);

            builder
                .HasOne(cb => cb.Equipe)
                .WithMany()
                .HasForeignKey(ci => ci.EquipeId);

            builder
                .HasOne(cb => cb.Pedido)
                .WithMany()
                .HasForeignKey(ci => ci.PedidoId);
        }
    }
}
