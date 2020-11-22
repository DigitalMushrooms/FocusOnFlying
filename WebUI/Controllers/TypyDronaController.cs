using FocusOnFlying.Application.TypyDrona.PobierzTypyDrona;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/typydrona")]
    public class TypyDronaController : ApiController
    {
        [HttpGet]
        public async Task<List<TypDronaDto>> PobierzTypyDrona()
        {
            var query = new PobierzTypyDronaQuery();
            List<TypDronaDto> typyDrona = await Mediator.Send(query);
            return typyDrona;
        }
    }
}
