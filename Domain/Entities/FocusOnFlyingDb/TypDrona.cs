using System;
using System.Collections.Generic;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class TypDrona
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public ICollection<Dron> Drony { get; set; } = new List<Dron>();
    }
}
