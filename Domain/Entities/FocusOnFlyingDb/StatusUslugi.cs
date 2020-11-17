using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class StatusUslugi
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Usluga> Uslugi { get; set; } = new List<Usluga>();
    }
}
