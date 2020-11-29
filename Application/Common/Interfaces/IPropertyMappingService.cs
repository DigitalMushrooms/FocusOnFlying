using FocusOnFlying.Infrastructure.Services.PropertyMapping;
using System.Collections.Generic;

namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface IPropertyMappingService
    {
        Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSource, TDestination>();
    }
}
