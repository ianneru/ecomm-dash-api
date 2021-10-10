using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.EntityConfigurations
{
    public class ViewerEntityTypeConfiguration
        : IEntityTypeConfiguration<Viewer>
    {
        public void Configure(EntityTypeBuilder<Viewer> builder)
        {
            builder.ToTable("Viewers");

            builder.HasKey(ci => ci.Id);
        }
    }
}
