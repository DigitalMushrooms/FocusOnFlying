using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class StatusMisji
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }

        public Misja Misja { get; set; }
    }
}
