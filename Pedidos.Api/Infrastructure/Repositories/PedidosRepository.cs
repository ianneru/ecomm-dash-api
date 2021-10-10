using Microsoft.EntityFrameworkCore;
using Pedidos.API.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Pedidos.API.Infrastructure.Repositories
{
    public class PedidosRepository : BaseRepository<Pedido>, IPedidosRepository
    {
        public PedidosRepository(EcommContext contexto) : base(contexto)
        {
        }

        public async Task<IEnumerable<Pedido>> GetPedidos() =>  await dbSet.ToListAsync();
    }
}
