using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Drony.Commands.UtworzDrona;
using FocusOnFlying.Application.Drony.Queries.PobierzDrony;
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

        [HttpGet]
        public async Task<PagedResult<DronDto>> PobierzDrony([FromQuery] PobierzDronyQuery query)
        {
            PagedResult<DronDto> drony = await Mediator.Send(query);
            return drony;
        }
    }
}
