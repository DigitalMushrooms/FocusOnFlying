using FocusOnFlying.Application.TypyMisji.Queries.PobierzTypyMisji;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/typymisji")]
    [Authorize]
    public class TypyMisjiController : ApiController
    {
        [HttpGet]
        public async Task<List<TypMisjiDto>> PobierzTypyMisji()
        {
            var query = new PobierzTypyMisjiQuery();
            var typyMisji = await Mediator.Send(query);
            return typyMisji;
        }
    }
}
