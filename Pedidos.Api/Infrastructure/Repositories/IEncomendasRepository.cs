using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public interface IEncomendasRepository
    {
        IQueryable<Encomenda> Get();
    }
}
