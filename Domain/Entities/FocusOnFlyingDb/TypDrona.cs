using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class TypDrona : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        [JsonIgnore]
        public ICollection<Dron> Drony { get; set; } = new List<Dron>();
    }
}
