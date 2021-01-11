﻿using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Dron : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string NumerSeryjny { get; set; }
        public Guid IdTypuDrona { get; set; }
        public DateTime DataNastepnegoPrzegladu { get; set; }

        [JsonIgnore]
        public TypDrona TypDrona { get; set; }
        [JsonIgnore]
        public ICollection<MisjaDron> MisjeDrony { get; set; } = new List<MisjaDron>();
    }
}
