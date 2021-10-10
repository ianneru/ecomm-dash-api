using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pedidos.API.Infrastructure.Repositories;
using Pedidos.API.Model;
using Pedidos.API.Services;
using Pedidos.API.ViewModel;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Pedidos.API.Controllers
{
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly ILogger<PedidosController> _logger;

        public IPedidoService _service { get; }

        public PedidosController(
            ILogger<PedidosController> logger,
            IPedidoService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet()]
        [Authorize]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Pedido>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<Pedido>>> GetPedidos(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0)
        {
            var produtos = await _service.GetPaginated(pageSize,pageIndex);

            return Ok(produtos);
        }
    }
}
