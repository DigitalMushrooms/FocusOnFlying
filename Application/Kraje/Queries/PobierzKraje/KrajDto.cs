using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.Kraje.Queries.PobierzKraje
{
    public class KrajDto : IMapFrom<Kraj>
    {
        public Guid Id { get; set; }
        public string NazwaKraju { get; set; }
        public string Skrot { get; set; }
    }
}
