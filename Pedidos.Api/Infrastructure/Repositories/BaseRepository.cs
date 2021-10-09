using Microsoft.EntityFrameworkCore;
using Pedidos.API.Model;

namespace Pedidos.API.Infrastructure.Repositories
{
    public abstract class BaseRepository<T> where T : BaseModel
    {
        protected readonly DbContext contexto;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(DbContext contexto)
        {
            this.contexto = contexto;

            dbSet = contexto.Set<T>();
        }
    }
}
