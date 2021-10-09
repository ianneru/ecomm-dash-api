using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Pedidos.API.Model
{
    public class Produto : BaseModel
    {
        public Produto()
        {

        }
        
        public Produto(string Nome,string Descricao,decimal Valor)
        {
            this.Nome = Nome;
            this.Descricao = Descricao;
            this.Valor = Valor;
        }

        [DataMember]
        public string Nome { get; set; }

        [DataMember]
        public string Descricao { get; set; }

        [DataMember]
        public decimal Valor { get; set; }

        public ICollection<PedidoProduto> PedidosProdutos { get; set; }
    }
}
