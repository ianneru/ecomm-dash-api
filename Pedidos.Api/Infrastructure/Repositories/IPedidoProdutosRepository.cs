using Pedidos.API.Model;
using System.Collections.Generic;

namespace Pedidos.API.Infrastructure.Repositories
{
    public interface IPedidoProdutosRepository
    {
        ICollection<PedidoProduto> GetByPedido(int id);
    }
}
