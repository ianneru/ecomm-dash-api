using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pedidos.API.Infrastructure.Repositories;
using Pedidos.API.Model;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Pedidos.API.Controllers
{
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;

        public IPedidosRepository _repository { get; }

        public PedidosController(
            ILogger<PedidosController> logger,
            IPedidosRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        /// <summary>
        /// Obtém a lista completa de pedidos.
        /// </summary>
        /// <returns>
        /// A lista completa de produtos do catálogo
        /// </returns>
        /// <response code="401">Não autorizado</response> 
        [HttpGet("{pesquisa}")]
        [ProducesResponseType(typeof(IEnumerable<Pedido>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Pedido>>> GetPedidos()
        {
            var produtos = await _repository.GetPedidos();

            return Ok(produtos);
        }
    }
}
