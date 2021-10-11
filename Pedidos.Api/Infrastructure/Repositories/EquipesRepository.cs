using Pedidos.API.Model;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class EquipesRepository : BaseRepository<Equipe>, IEquipesRepository
    {
        public EquipesRepository(EcommContext contexto) : base(contexto)
        {
        }

        public IQueryable<Equipe> Get() =>  dbSet.AsQueryable();
    }
}
