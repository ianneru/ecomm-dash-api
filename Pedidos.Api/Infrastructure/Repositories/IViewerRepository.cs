using Pedidos.API.Model;
using System.Threading.Tasks;

namespace Pedidos.API.Infrastructure.Repositories
{
    public interface IViewerRepository
    {
        Task<Viewer> GetByUserNameAndPassword(string userName, string password);
    }
}
