using FocusOnFlying.Application.Misje.Queries;
using FocusOnFlying.Application.Models;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/misje")]
    [Authorize]
    public class MisjeController : ApiController
    {
        [HttpGet]
        public async Task<PagedResult<MisjaDto>> PobierzMisje()
        {
            var query = new PobierzMisjeQuery();
            var misje = await Mediator.Send(query);
            return misje;
        }
    }
}
