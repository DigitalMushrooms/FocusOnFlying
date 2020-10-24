﻿using FocusOnFlying.Application.Klienci.Commands.UtworzKlienta;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FocusOnFlying.WebUI.Controllers
{
    [Route("/api/klienci")]
    public class KlienciController : ApiController
    {
        [HttpGet]
        public async Task<List<KlientDto>> PobierzKlientow()
        {
            var query = new PobierzKlientowQuery();
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
