using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Uslugi.Commands.UtworzMisjeUslugi;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Application.Uslugi.Commands.ZaktualizujUsluge;
using FocusOnFlying.Application.Uslugi.Queries.PobierzMisjeUslugi;
using FocusOnFlying.Application.Uslugi.Queries.PobierzUslugi;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
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
        public async Task<PagedResult<UslugaDto>> PobierzUslugi([FromQuery] PobierzUslugiQuery query)
        {
            PagedResult<UslugaDto> uslugi = await Mediator.Send(query);
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

        [HttpPost("{id}/misje")]
        public async Task UtworzMisjeUslugi([FromRoute] Guid id, [FromBody] UtworzMisjeUslugiCommand command)
        {
            command.Id = id;
            await Mediator.Send(command);
        }

        [HttpPatch("{id}")]
        public async Task ZaktualizujUsluge(
            [FromRoute] Guid id,
            [FromBody][JsonSchemaType(typeof(List<Operation>))] JsonPatchDocument<UslugaUpdateDto> patch)
        {
            var command = new ZaktualizujUslugeCommand { Id = id, Patch = patch };
            await Mediator.Send(command);
        }
    }
}
