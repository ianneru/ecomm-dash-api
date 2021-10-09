namespace Pedidos.API.Model
{
    public class PedidoProduto : BaseModel
    {
        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; }

        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }
    }
}
