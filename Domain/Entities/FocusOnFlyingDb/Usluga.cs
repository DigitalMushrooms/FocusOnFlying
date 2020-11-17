using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Usluga
    {
        public Guid Id { get; set; }
        public DateTime DataPrzyjeciaZlecenia { get; set; }
        public Guid IdKlienta { get; set; }
        public Guid IdStatusuUslugi { get; set; }

        public Klient Klient { get; set; }
        public StatusUslugi StatusUslugi { get; set; }
        public ICollection<Misja> Misje { get; set; } = new List<Misja>();
    }
}
