using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Usluga
    {
        public Guid Id { get; set; }
        public DateTime DataPrzyjeciaZlecenia { get; set; }
        public Guid IdKlienta { get; set; }
        public Klient Klient { get; set; }
    }
}
