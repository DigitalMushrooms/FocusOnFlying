using System;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Dron
    {
        public Guid Id { get; set; }
        public string Producent { get; set; }
        public string Model { get; set; }
        public string NumerSeryjny { get; set; }
        public Guid IdTypuDrona { get; set; }
        public DateTime DataNastepnegoPrzegladu { get; set; }
    }
}
