using FocusOnFlying.Application.Common.Mappings;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using System;

namespace FocusOnFlying.Application.StatusyUslugi.Queries.PobierzStatusUslugi
{
    public class StatusUslugiDto : IMapFrom<StatusUslugi>
    {
        public Guid Id { get; set; }
        public string Nazwa { get; set; }
    }
}
