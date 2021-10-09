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
        public DbSet<Equipe> Equipes { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProdutoEntityTypeConfiguration());
            
            builder.ApplyConfiguration(new EquipeEntityTypeConfiguration());
            
            builder.ApplyConfiguration(new PedidoProdutoEntityTypeConfiguration());
            
            builder.ApplyConfiguration(new PedidoEntityTypeConfiguration());

            builder.ApplyConfiguration(new EncomendaEntityTypeConfiguration());
        }
    }


    public class CatalogContextDesignFactory : IDesignTimeDbContextFactory<EcommContext>
    {
        public EcommContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EcommContext>()
                .UseSqlite("Data Source=Ecommerce.db;");

            return new EcommContext(optionsBuilder.Options);
        }
    }
}
