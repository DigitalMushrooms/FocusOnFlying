using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Kraj
    {
        public Guid Id { get; set; }
        public string NazwaKraju { get; set; }
        public string Skrot { get; set; }
        public ICollection<Klient> Klienci { get; set; } = new List<Klient>();
    }
}
