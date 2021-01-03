using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Faktura
    {
        public Guid Id { get; set; }
        public string NumerFaktury { get; set; }
        public decimal WartoscNetto { get; set; }
        public decimal WartoscBrutto { get; set; }
        public bool ZaplaconaFaktura { get; set; }

        public Usluga Usluga { get; set; }
    }
}
