using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class TypMisji : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        [JsonIgnore]
        public ICollection<Misja> Misje { get; set; } = new List<Misja>();
    }
}
