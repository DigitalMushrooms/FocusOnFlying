using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Kraj : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public string NazwaKraju { get; set; }
        public string Skrot { get; set; }

        [JsonIgnore]
        public ICollection<Klient> Klienci { get; set; } = new List<Klient>();
    }
}
