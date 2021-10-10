using Pedidos.API.Model;
using Pedidos.API.ViewModel;
using System.Threading.Tasks;

namespace Pedidos.API.Services
{
    public interface IPedidoService
    {
        ValueTask<PaginatedItemsViewModel<Pedido>> GetPaginated(int pageSize, int pageIndex);
    }
}