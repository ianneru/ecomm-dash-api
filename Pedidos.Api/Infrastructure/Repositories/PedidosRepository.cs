using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class PedidosRepository : BaseRepository<Pedido>, IPedidosRepository
    {
        public PedidosRepository(EcommContext contexto) : base(contexto)
        {
        }

        public IQueryable<Pedido> Get() =>  dbSet.AsQueryable();
    }
}
