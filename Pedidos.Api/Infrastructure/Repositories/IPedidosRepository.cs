using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public interface IPedidosRepository
    {
        IQueryable<Pedido> Get();
    }
}
