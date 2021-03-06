using Pedidos.API.Model;
using Pedidos.API.ViewModel;
using System.Threading.Tasks;

namespace Pedidos.API.Services
{
    public interface IEncomendaService
    {
        ValueTask<PaginatedItemsViewModel<Encomenda>> GetPaginated(int pageSize, int pageIndex);
    }
}