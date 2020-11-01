using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.StatusyMisji.Queries.PobierzStatusyMisji
{
    public class StatusMisjiDto : IMapFrom<StatusMisji>
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
    }
}
