using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class MisjaDron
    {
        public Guid IdMisji { get; set; }
        public Guid IdDrona { get; set; }

        public Misja Misja { get; set; }
        public Dron Dron { get; set; }
    }
}
