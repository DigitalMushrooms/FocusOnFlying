using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/uslugi")]
    [Authorize]
    public class UslugiController : ApiController
    {
        [HttpPost]
        public async Task UtworzUsluge(UtworzonaUslugaCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
