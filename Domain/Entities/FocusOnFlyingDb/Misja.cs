using FocusOnFlying.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace FocusOnFlying.Domain.Entities.FocusOnFlyingDb
{
    public class Misja : IAudytowalnaTabela
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
        public string Opis { get; set; }
        public Guid IdTypuMisji { get; set; }
        public int MaksymalnaWysokoscLotu { get; set; }
        public Guid IdStatusuMisji { get; set; }
        public DateTime? DataRozpoczecia { get; set; }
        public DateTime? DataZakonczenia { get; set; }
        public Guid IdUslugi { get; set; }
        public string IdPracownika { get; set; }

        public decimal SzerokoscGeograficzna { get; set; }
        public decimal DlugoscGeograficzna { get; set; }
        public int Promien { get; set; }
        
        [JsonIgnore]
        public TypMisji TypMisji { get; set; }
        [JsonIgnore]
        public StatusMisji StatusMisji { get; set; }
        [JsonIgnore]
        public Usluga Usluga { get; set; }
        [JsonIgnore]
        public ICollection<MisjaDron> MisjeDrony { get; set; } = new List<MisjaDron>();
    }
}
