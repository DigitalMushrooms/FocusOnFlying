﻿using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class StatusMisji
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Misja> Misje { get; set; } = new List<Misja>();
    }
}
