using Pedidos.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pedidos.API.Infrastructure.Repositories
{
    public interface IPedidosRepository
    {
        Task<IEnumerable<Pedido>> GetPedidos();
    }
}
