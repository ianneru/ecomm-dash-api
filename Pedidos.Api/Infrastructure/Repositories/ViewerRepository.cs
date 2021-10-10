using Microsoft.EntityFrameworkCore;
using Pedidos.API.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class ViewerRepository : BaseRepository<Viewer>, IViewerRepository
    {
        public ViewerRepository(EcommContext contexto) : base(contexto)
        {
        }

        public async Task<Viewer> GetByUserNameAndPassword(string userName, string password)
            => await dbSet
                    .FirstOrDefaultAsync(o => o.Username == userName && o.Password == password);
    }
}
