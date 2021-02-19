using FocusOnFlying.Application.Audyty.Queries.PobierzAudyty;
using FocusOnFlying.Application.Common.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/audyty")]
    [Authorize]
    public class AudytyController : ApiController
    {
        [HttpGet]
        public async Task<PagedResult<AudytDto>> PobierzAudyty([FromQuery] PobierzAudytyQuery query)
        {
            return await Mediator.Send(query);
        }
    }
}
