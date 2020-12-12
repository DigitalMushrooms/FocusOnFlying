using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Klienci.Commands.UtworzKlienta;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/klienci")]
    [Authorize(Roles = "Pracownik")]
    public class KlienciController : ApiController
    {
        [HttpGet]
        public async Task<PagedResult<KlientDto>> PobierzKlientow([FromQuery] PobierzKlientowQuery query)
        {
            var klienci = await Mediator.Send(query);
            return klienci;
        }

        [HttpPost]
        public async Task UtworzKlienta(UtworzKlientaCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
