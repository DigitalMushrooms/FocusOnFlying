using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Application.Klienci.Queries.PobierzKlientow;
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

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<KlientDto, Klient>(_klientMapping));
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
