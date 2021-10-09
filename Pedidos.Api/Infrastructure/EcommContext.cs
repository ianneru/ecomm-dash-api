using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Pedidos.API.Infrastructure.EntityConfigurations;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure
{
    public class EcommContext : DbContext
    {
        public EcommContext(DbContextOptions<EcommContext> options) : base(options)
        {
        }

        public DbSet<Pedido> Pedidos { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PedidoEntityTypeConfiguration());
        }
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<EcommContext>
    {
        public EcommContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EcommContext>()
                .UseSqlite("Data Source=PedidosDB.db;");

            return new EcommContext(optionsBuilder.Options);
        }
    }
}
