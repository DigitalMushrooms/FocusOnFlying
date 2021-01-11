using FocusOnFlying.Domain.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Usluga : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public DateTime DataPrzyjeciaZlecenia { get; set; }
        public Guid? IdFaktury { get; set; }
        public Guid IdKlienta { get; set; }
        public Guid IdStatusuUslugi { get; set; }

        [JsonIgnore]
        public Klient Klient { get; set; }
        [JsonIgnore]
        public Faktura Faktura { get; set; }
        [JsonIgnore]
        public StatusUslugi StatusUslugi { get; set; }
        [JsonIgnore]
        public ICollection<Misja> Misje { get; set; } = new List<Misja>();
    }
}
