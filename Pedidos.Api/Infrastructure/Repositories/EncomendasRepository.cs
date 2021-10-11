using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class EncomendasRepository : BaseRepository<Encomenda>, IEncomendasRepository
    {
        public EncomendasRepository(EcommContext contexto) : base(contexto)
        {
        }

        public IQueryable<Encomenda> Get() =>  dbSet.AsQueryable();
    }
}
