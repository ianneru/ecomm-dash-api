using Microsoft.EntityFrameworkCore;
using Pedidos.API.Infrastructure.Repositories;
using Pedidos.API.Model;
using Pedidos.API.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.API.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidosRepository _pedidosRepository;

        public PedidoService(IPedidosRepository pedidosRepository)
        {
            _pedidosRepository = pedidosRepository;
        }

        public async ValueTask<PaginatedItemsViewModel<Pedido>> GetPaginated(int pageSize = 10, int pageIndex = 0)
        {
            var pedidos =  _pedidosRepository.Get();

            var totalItems = await pedidos.LongCountAsync();

            var itemsOnPage = await pedidos
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .ToListAsync();

            return new PaginatedItemsViewModel<Pedido>(pageIndex, pageSize, totalItems, itemsOnPage);
        }
    }
}
