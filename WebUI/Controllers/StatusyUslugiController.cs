using FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusUslugi;
using Microsoft.AspNetCore.Mvc;
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
    }
}
