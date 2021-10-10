using Pedidos.API.Model;
using System.Threading.Tasks;

namespace Pedidos.API.Services
{
    public interface IViewerService
    {
        ValueTask<Viewer> Authenticate(string username, string password);
    }
}