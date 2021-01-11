using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Audyt
    {
        public Guid Id { get; set; }
        public Guid IdAudytowanegoWiersza { get; set; }
        public string NazwaTabeli { get; set; }
        public string Dane { get; set; }
        public DateTime DataAudytu { get; set; }
        public string Uzytkownik { get; set; }
        public string TypOperacji { get; set; }
    }
}
