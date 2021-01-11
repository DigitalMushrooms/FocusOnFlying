using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class StatusUslugi : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        [JsonIgnore]
        public ICollection<Usluga> Uslugi { get; set; } = new List<Usluga>();
    }
}
