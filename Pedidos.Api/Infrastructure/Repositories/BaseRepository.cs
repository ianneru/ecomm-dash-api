using Microsoft.EntityFrameworkCore;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly EcommContext contexto;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(EcommContext contexto)
        {
            this.contexto = contexto;

            dbSet = contexto.Set<T>();
        }
    }
}
