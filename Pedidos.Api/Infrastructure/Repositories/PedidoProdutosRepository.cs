using Pedidos.API.Model;
using System.Collections.Generic;
using System.Linq;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class PedidoProdutosRepository : BaseRepository<PedidoProduto>, IPedidoProdutosRepository
    {
        public PedidoProdutosRepository(EcommContext contexto) : base(contexto)
        {
        }

        public ICollection<PedidoProduto> GetByPedido(int id) =>  dbSet.Where(o=>o.PedidoId == id).ToList();
    }
}
