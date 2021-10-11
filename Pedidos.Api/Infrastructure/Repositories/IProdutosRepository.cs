using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public interface IProdutosRepository
    {
        IQueryable<Produto> Get();
    }
}
