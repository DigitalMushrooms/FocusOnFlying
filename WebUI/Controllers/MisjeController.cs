﻿using FocusOnFlying.Application.Common.Models;
using FocusOnFlying.Application.Misje.Commands.UsunMisje;
using FocusOnFlying.Application.Misje.Commands.ZaktualizujMisje;
using FocusOnFlying.Application.Misje.Queries;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
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
    [Route("api/misje")]
    [Authorize]
    public class MisjeController : ApiController
    {
        [HttpGet]
        public async Task<PagedResult<MisjaDto>> PobierzMisje([FromQuery] PobierzMisjeQuery query)
        {
            var misje = await Mediator.Send(query);
            return misje;
        }

        [HttpPatch("{id}")]
        public async Task ZaktualizujMisje(
            [FromRoute] Guid id,
            [FromBody] [JsonSchemaType(typeof(List<Operation>))] JsonPatchDocument<MisjaUpdateDto> patch)
        {
            var command = new ZaktualizujMisjeCommand { Id = id, Patch = patch };
            await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task UsunMisje(Guid id)
        {
            var command = new UsunMisjeCommand { Id = id };
            await Mediator.Send(command);
        }
    }
}
