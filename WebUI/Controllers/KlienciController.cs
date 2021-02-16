using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Klienci.Commands.UtworzKlienta;
using FocusOnFlying.Application.Klienci.Commands.ZaktualizujKlienta;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlienta;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/klienci")]
    public class KlienciController : ApiController
    {
        [HttpGet]
        public async Task<PagedResult<KlientDto>> PobierzKlientow([FromQuery] PobierzKlientowQuery query)
        {
            var klienci = await Mediator.Send(query);
            return klienci;
        }

        [HttpGet("{id}")]
        public async Task<KlientDto> PobierzKlienta(Guid id)
        {
            var klient = await Mediator.Send(new PobierzKlientaQuery { Id = id });
            return klient;
        }

        [HttpPost]
        public async Task UtworzKlienta(UtworzKlientaCommand command)
        {
            await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task ZaktualizujKlienta([FromRoute] Guid id, [FromBody] ZaktualizujKlientaCommand command)
        {
            command.Id = id;
            await Mediator.Send(command);
        }
    }
}
