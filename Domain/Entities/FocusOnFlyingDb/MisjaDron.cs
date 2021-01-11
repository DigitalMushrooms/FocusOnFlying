using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class MisjaDron : IAudytowalnaTabela
    {
        public Guid IdMisji { get; set; }
        public Guid IdDrona { get; set; }

        [JsonIgnore]
        public Misja Misja { get; set; }
        [JsonIgnore]
        public Dron Dron { get; set; }
    }
}
