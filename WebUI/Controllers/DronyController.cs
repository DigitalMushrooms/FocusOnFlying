using FocusOnFlying.Application.Drony.Commands.UtworzDrona;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/drony")]
    [Authorize]
    public class DronyController : ApiController
    {
        [HttpPost]
        public async Task UtworzDrona(UtworzDronaCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
