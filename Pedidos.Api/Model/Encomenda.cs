using System.Runtime.Serialization;

namespace Pedidos.API.Model
{
    public class Encomenda : BaseModel
    {
        public Encomenda()
        {

        }
        
        public Encomenda(int EquipeId,int PedidoId,string PlacaVeiculo)
        {
            this.EquipeId = EquipeId;
            this.PedidoId = PedidoId;
            this.PlacaVeiculo = PlacaVeiculo;
        }

        [DataMember]
        public int EquipeId { get; set; }

        public Equipe Equipe { get; set; }

        [DataMember]
        public int PedidoId { get; set; }

        public Pedido Pedido { get; set; }

        [DataMember]
        public string PlacaVeiculo { get; set; }
    }
}
