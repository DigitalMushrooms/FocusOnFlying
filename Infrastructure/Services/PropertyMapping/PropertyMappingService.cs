using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Drony.Queries.PobierzDrony;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
using FocusOnFlying.Application.Uslugi.Commands.UtworzUsluge;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FocusOnFlying.Infrastructure.Services.PropertyMapping
{
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _klientMapping = new Dictionary<string, PropertyMappingValue>
        {
            { "imie", new PropertyMappingValue(new[] { "Imie" }) },
            { "nazwisko", new PropertyMappingValue(new[] { "Nazwisko" }) },
            { "pesel", new PropertyMappingValue(new[] { "Pesel" }) },
            { "nazwa", new PropertyMappingValue(new[] { "Nazwa" }) },
            { "regon", new PropertyMappingValue(new[] { "Regon" }) },
            { "nip", new PropertyMappingValue(new[] { "Nip" }) },
            { "adres", new PropertyMappingValue(new[] { "Ulica" }) },
            { "kraj.nazwaKraju", new PropertyMappingValue(new[] { "Kraj.NazwaKraju" }) }
        };

        private readonly Dictionary<string, PropertyMappingValue> _dronMapping = new Dictionary<string, PropertyMappingValue>
        {
            { "producent", new PropertyMappingValue(new[] { "Producent" }) },
            { "model", new PropertyMappingValue(new[] { "Model" }) },
            { "numerSeryjny", new PropertyMappingValue(new[] { "NumerSeryjny" }) }
        };

        private readonly Dictionary<string, PropertyMappingValue> _misjaMapping = new Dictionary<string, PropertyMappingValue>
        {
            { "nazwa", new PropertyMappingValue(new[] { "Nazwa" }) },
            { "opis", new PropertyMappingValue(new[] { "Opis" }) },
            { "typMisji.nazwa", new PropertyMappingValue(new[] { "TypMisji.Nazwa" }) },
            { "dataRozpoczecia", new PropertyMappingValue(new[] { "DataRozpoczecia" }) },
            { "dataZakonczenia", new PropertyMappingValue(new[] { "DataZakonczenia" }) }
        };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<KlientDto, Klient>(_klientMapping));
            _propertyMappings.Add(new PropertyMapping<DronDto, Dron>(_dronMapping));
            _propertyMappings.Add(new PropertyMapping<MisjaDto, Misja>(_misjaMapping));
        }

        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>()
        {
            IEnumerable<PropertyMapping<TSource, TDestination>> propertyMappings = _propertyMappings
                .OfType<PropertyMapping<TSource, TDestination>>()
                .ToList();

            if (propertyMappings.Any())
            {
                return propertyMappings.Single().MappingDictionary;
            }

            throw new Exception($"Nie znaleziono dokładnego dopasowania dla instancji <{typeof(TSource)},{typeof(TDestination)}>");
        }
    }
}
