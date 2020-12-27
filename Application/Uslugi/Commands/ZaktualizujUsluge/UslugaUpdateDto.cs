using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using NSwag.Annotations;
using System;

namespace FocusOnFlying.Application.Uslugi.Commands.ZaktualizujUsluge
{
    public class UslugaUpdateDto : IMapFrom<Usluga>
    {
        [OpenApiIgnore]
        public Guid Id { get; set; }
        public Guid IdStatusuUslugi { get; set; }
    }
}
