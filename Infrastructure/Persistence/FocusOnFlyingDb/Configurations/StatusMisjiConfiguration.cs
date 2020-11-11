using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class StatusMisjiConfiguration : IEntityTypeConfiguration<StatusMisji>
    {
        public void Configure(EntityTypeBuilder<StatusMisji> builder)
        {
            builder.ToTable("StatusyMisji");

            builder.Property(x => x.Id);
            builder.Property(x => x.Nazwa).IsRequired();

            builder.HasData(new StatusMisji { Id = new Guid("b59173ac-0606-47c4-9ddf-0af363d564de"), Nazwa = "Utworzona" });
            builder.HasData(new StatusMisji { Id = new Guid("beb37d80-9eeb-483c-ab5b-a95c30f6f1ce"), Nazwa = "Zaplanowana" });
            builder.HasData(new StatusMisji { Id = new Guid("c3505f48-abd3-43f8-98cf-439129cc4194"), Nazwa = "Anulowana" });
            builder.HasData(new StatusMisji { Id = new Guid("34e560c8-d677-4292-aaaf-af1fd9d4e8e0"), Nazwa = "Wykonana" });
        }
    }
}
