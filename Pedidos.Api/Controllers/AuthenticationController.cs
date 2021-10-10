using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pedidos.API.Services;
using Pedidos.API.ViewModel;
using System.Threading.Tasks;

namespace Pedidos.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly ITokenService _tokenService;
        public readonly IViewerService _viewerService;

        public AuthenticationController(
            IViewerService viewerService,
            ITokenService tokenService)
        {
            _tokenService = tokenService;
            _viewerService = viewerService;
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<dynamic>> Authenticate(AuthenticationInput authenticationInput)
        {
            var viewer = await _viewerService.Authenticate(authenticationInput.Username, authenticationInput.Password);

            if (viewer == default)
                return Unauthorized(new {message = "Usuário inválido"});

            var token = _tokenService.GenerateToken(viewer);

            return new {viewer, token};
        }
    }
}