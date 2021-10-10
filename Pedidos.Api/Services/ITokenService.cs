using Pedidos.API.Model;

namespace Pedidos.API.Services
{
    public interface ITokenService
    {
        string GenerateToken(Viewer viewer);
    }
}