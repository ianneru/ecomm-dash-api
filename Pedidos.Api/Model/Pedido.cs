using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;

namespace Pedidos.API.Model
{
    public class Pedido : BaseModel
    {
        public Pedido()
        {

        }
        
        public Pedido(string NumeroIdentificacao,
            DateTime? DataEntrega,string Endereco,
            ICollection<Produto> Produtos)
        {
            this.NumeroIdentificacao = NumeroIdentificacao;
            
            DataCriacao = DateTime.Now;
            
            this.DataEntrega = DataEntrega;
            
            this.Endereco = Endereco;

            var listaPedidosProdutos = new List<PedidoProduto>();

            foreach(var produto in Produtos)
            {
                listaPedidosProdutos.Add(new PedidoProduto
                {
                    Produto = produto
                });
            }

            this.PedidosProdutos = listaPedidosProdutos;
        }

        [DataMember]
        public string NumeroIdentificacao { get; set; }

        [Required]
        [DataMember]
        public DateTime DataCriacao { get; set; }

        [DataMember]
        public DateTime? DataEntrega { get; set; }

        [Required]
        public string Endereco { get; set; }

        public ICollection<PedidoProduto> PedidosProdutos { get; set; } = new List<PedidoProduto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!PedidosProdutos.Any())
            {
                results.Add(new ValidationResult("Número inválido de produtos", new[] { "Quantidade" }));
            }

            return results;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
