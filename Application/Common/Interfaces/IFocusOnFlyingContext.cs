﻿using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;

namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface IFocusOnFlyingContext
    {
        public DbSet<Klient> Klienci { get; set; }
    }
}