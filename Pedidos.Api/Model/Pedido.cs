using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Pedidos.API.Model
{
    public class Pedido : BaseModel
    {
        [Required]
        public DateTime DataCriacao { get; set; }

        public DateTime? DataEntrega { get; set; }

        [Required]
        public string Endereco { get; set; }

        public ICollection<Produto> Produtos { get; set; } = new List<Produto>();

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (!Produtos.Any())
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
