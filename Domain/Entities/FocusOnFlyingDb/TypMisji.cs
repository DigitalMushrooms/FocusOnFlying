﻿using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class TypMisji
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public List<Misja> Misje { get; set; }
    }
}
