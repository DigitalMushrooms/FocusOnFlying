using FocusOnFlying.Application.Faktury.Commands.UsunFakture;
using FocusOnFlying.Application.Faktury.Commands.ZaktualizujFakture;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.AspNetCore.Mvc;
using NJsonSchema.Annotations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/faktury")]
    public class FakturyController : ApiController
    {
        [HttpPatch("{id}")]
        public async Task ZaktualizujFakture(
            [FromRoute] Guid id,
            [FromBody][JsonSchemaType(typeof(List<Operation>))] JsonPatchDocument<FakturaUpdateDto> patch)
        {
            var command = new ZaktualizujFaktureCommand { Id = id, Patch = patch };
            await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task UsunFakture(Guid id)
        {
            var command = new UsunFaktureCommand { Id = id };
            await Mediator.Send(command);
        }
    }
}
