using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class ProdutosRepository : BaseRepository<Produto>, IProdutosRepository
    {
        public ProdutosRepository(EcommContext contexto) : base(contexto)
        {
        }

        public IQueryable<Produto> Get() =>  dbSet.AsQueryable();
    }
}
