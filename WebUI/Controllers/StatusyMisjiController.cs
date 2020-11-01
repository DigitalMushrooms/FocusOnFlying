using FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusMisji;
using FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusyMisji;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/statusymisji")]
    public class StatusyMisjiController : ApiController
    {
        [HttpGet]
        public async Task<List<StatusMisjiDto>> PobierzStatusyMisji()
        {
            var query = new PobierzStatusyMisjiQuery();
            List<StatusMisjiDto> statusyMisji = await Mediator.Send(query);
            return statusyMisji;
        }

        [HttpGet("{nazwa}")]
        public async Task<StatusMisjiDto> PobierzStatusMisji(string nazwa)
        {
            var query = new PobierzStatusMisjiQuery { Nazwa = nazwa };
            StatusMisjiDto statusyMisji = await Mediator.Send(query);
            return statusyMisji;
        }
    }
}
