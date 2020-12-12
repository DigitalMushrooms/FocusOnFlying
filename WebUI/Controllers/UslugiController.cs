using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Application.Uslugi.Queries.PobierzMisjeUslugi;
using FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/uslugi")]
    [Authorize]
    public class UslugiController : ApiController
    {
        [HttpGet]
        public async Task<List<UslugaDto>> PobierzUslugi()
        {
            var query = new PobierzUslugiQuery();
            List<UslugaDto> uslugi = await Mediator.Send(query);
            return uslugi;
        }

        [HttpGet("{id}/misje")]
        public async Task<PagedResult<MisjaDto>> PobierzMisjeUslugi([FromRoute] Guid id, [FromQuery] PobierzMisjeUslugiQuery query)
        {
            query.Id = id;
            PagedResult<MisjaDto> misje = await Mediator.Send(query);
            return misje;
        }

        [HttpPost]
        public async Task UtworzUsluge(UtworzUslugeCommand command)
        {
            await Mediator.Send(command);
        }
    }
}
