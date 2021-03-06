﻿using FocusOnFlying.Application.Kraje.Queries.PobierzKraje;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("api/kraje")]
    [Authorize(Roles = "Pracownik")]
    public class KrajeController : ApiController
    {
        [HttpGet]
        public async Task<List<KrajDto>> PobierzKraje([FromQuery] PobierzKrajeQuery query)
        {
            List<KrajDto> kraje = await Mediator.Send(query);
            return kraje;
        }
    }
}
