﻿using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    public class KlienciController : ApiController
    {
        [HttpGet]
        public async Task<List<KlienciDto>> PobierzKlientow()
        {
            var query = new PobierzKlientowQuery();
            var klienci = await Mediator.Send(query);

            return klienci;
        }
    }
}