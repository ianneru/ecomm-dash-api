using Microsoft.EntityFrameworkCore;
using Pedidos.API.Infrastructure.Repositories;
using Pedidos.API.Model;
using Pedidos.API.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pedidos.API.Services
{
    public class EncomendasService : IEncomendaService
    {
        private readonly IEncomendasRepository _encomendasRepository;
        private readonly IPedidosRepository _pedidosRepository;
        private readonly IEquipesRepository _equipesRepository;
        private readonly IProdutosRepository _produtosRepository;
        private readonly IPedidoProdutosRepository _pedidoProdutosRepository;

        public EncomendasService(IEncomendasRepository encomendasRepository,
            IPedidosRepository pedidosRepository,
            IEquipesRepository equipesRepository,
            IProdutosRepository produtosRepository,
            IPedidoProdutosRepository pedidoProdutosRepository)
        {
            _encomendasRepository = encomendasRepository;
            _pedidosRepository = pedidosRepository;
            _equipesRepository = equipesRepository;
            _produtosRepository = produtosRepository;
            _pedidoProdutosRepository = pedidoProdutosRepository;
        }

        public async ValueTask<PaginatedItemsViewModel<Encomenda>> GetPaginated(int pageSize = 5, int pageIndex = 0)
        {
            var encomendas = _encomendasRepository.Get();

            var totalItems = await encomendas.LongCountAsync();

            var itemsOnPage = await encomendas
                                    .Skip(pageSize * pageIndex)
                                    .Take(pageSize)
                                    .OrderBy(o => o.Pedido.DataCriacao)
                                    .ToListAsync();

            foreach (var encomenda in itemsOnPage)
            {
                encomenda.Pedido = _pedidosRepository.Get()
                                                     .FirstOrDefault(o => o.Id == encomenda.PedidoId);
                
                encomenda.Pedido.PedidosProdutos = _pedidoProdutosRepository.GetByPedido(encomenda.PedidoId);

                foreach (var pedidoProduto in encomenda.Pedido.PedidosProdutos)
                {
                    pedidoProduto.Produto = _produtosRepository.Get()
                                                .FirstOrDefault(o => o.Id == pedidoProduto.ProdutoId);

                    pedidoProduto.Pedido = null;
                    pedidoProduto.Produto.PedidosProdutos = null;
                }

                encomenda.Equipe = _equipesRepository.Get()
                                     .FirstOrDefault(o => o.Id == encomenda.EquipeId);
            }

            return new PaginatedItemsViewModel<Encomenda>(pageIndex, pageSize, totalItems, itemsOnPage);
        }
    }
}
