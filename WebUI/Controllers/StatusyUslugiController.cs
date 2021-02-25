using FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusUslugi;
using FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusyUslugi;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/statusyuslugi")]
    public class StatusyUslugiController : ApiController
    {
        [HttpGet("{nazwa}")]
        public async Task<StatusUslugiDto> PobierzStatusUslugi(string nazwa)
        {
            var query = new PobierzStatusUslugiQuery { Nazwa = nazwa };
            var statusUslugi = await Mediator.Send(query);
            return statusUslugi;
        }

        [HttpGet]
        public async Task<List<StatusUslugiDto>> PobierzStatusyUslugi()
        {
            var query = new PobierzStatusyUslugiQuery();
            var statusyUslugi = await Mediator.Send(query);
            return statusyUslugi;
        }
    }
}
