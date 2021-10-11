using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pedidos.API.Model;
using Pedidos.API.Services;
using Pedidos.API.ViewModel;
using System.Net;
using System.Threading.Tasks;

namespace Pedidos.API.Controllers
{
    [Route("api/[controller]")]
    public class EncomendasController : ControllerBase
    {
        private readonly ILogger<EncomendasController> _logger;

        public IEncomendaService _service { get; }

        public EncomendasController(
            ILogger<EncomendasController> logger,
            IEncomendaService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpGet()]
        [Authorize]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Pedido>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<PaginatedItemsViewModel<Pedido>>> GetEncomendas(
            [FromQuery] int pageSize = 5,
            [FromQuery] int pageIndex = 0)
        {
            var produtos = await _service.GetPaginated(pageSize,pageIndex);

            return Ok(produtos);
        }
    }
}
