using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class TypDronaConfiguration : IEntityTypeConfiguration<TypDrona>
    {
        public void Configure(EntityTypeBuilder<TypDrona> builder)
        {
            builder.ToTable("TypyDrona");

            builder.Property(x => x.Id);
            builder.Property(p => p.Nazwa).IsRequired();

            builder.HasData(new TypDrona { Id = new Guid("7f4ceb1e-2be1-418d-9c28-2fa11bae2429"), Nazwa = "Aircraft" });
            builder.HasData(new TypDrona { Id = new Guid("686bf52e-798b-42bf-bdfd-3bbd4936d6e8"), Nazwa = "Airship, Balloon" });
            builder.HasData(new TypDrona { Id = new Guid("39e46048-d0f5-479c-aebc-318d06c44d5e"), Nazwa = "Helicopter" });
            builder.HasData(new TypDrona { Id = new Guid("3170d6c9-59c4-486e-b392-61c07cc3a0da"), Nazwa = "Multi Rotor" });
        }
    }
}
