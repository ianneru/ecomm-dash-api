using Pedidos.API.Infrastructure.Repositories;
using Pedidos.API.Model;
using System;
using System.Threading.Tasks;

namespace Pedidos.API.Services
{
    public class ViewerService : IViewerService
    {
        private readonly IViewerRepository _viewerRepository;

        public ViewerService(IViewerRepository viewerRepository)
        {
            _viewerRepository = viewerRepository;
        }

        public async ValueTask<Viewer> Authenticate(string username, string password)
        {
            var viewer = await _viewerRepository.GetByUserNameAndPassword(username,password);

            if (viewer == default)
                return default;

            viewer.Password = string.Empty;

            return viewer;
        }
    }
}
