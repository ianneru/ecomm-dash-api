using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.EntityConfigurations
{
    public class PedidoProdutoEntityTypeConfiguration
        : IEntityTypeConfiguration<PedidoProduto>
    {
        public void Configure(EntityTypeBuilder<PedidoProduto> builder)
        {
            builder.ToTable("PedidosProdutos");

            builder.HasKey(o => o.Id);

            builder
                  .HasOne(bc => bc.Pedido)
                  .WithMany(b => b.PedidosProdutos)
                  .HasForeignKey(bc => bc.PedidoId);

            builder
                .HasOne(bc => bc.Produto)
                .WithMany(c => c.PedidosProdutos)
                .HasForeignKey(bc => bc.ProdutoId);
        }
    }
}
