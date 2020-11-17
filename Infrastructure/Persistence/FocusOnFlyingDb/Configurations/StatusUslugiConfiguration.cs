using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class StatusUslugiConfiguration : IEntityTypeConfiguration<StatusUslugi>
    {
        public void Configure(EntityTypeBuilder<StatusUslugi> builder)
        {
            builder.ToTable("StatusyUslugi");

            builder.Property(x => x.Id);
            builder.Property(x => x.Nazwa).IsRequired();

            builder.HasData(new StatusMisji { Id = new Guid("89407a86-a6d6-415a-b3bf-d3ee0b70ac85"), Nazwa = "Utworzona" });
            builder.HasData(new StatusMisji { Id = new Guid("ee545a45-a7ed-4aa9-9ac6-def05c93204f"), Nazwa = "W realizacji" });
            builder.HasData(new StatusMisji { Id = new Guid("eef8529f-9182-434b-957c-2df7462e2fbf"), Nazwa = "Zakończona" });
            builder.HasData(new StatusMisji { Id = new Guid("bdb1da1b-3713-46a9-8414-1c9a2e91f931"), Nazwa = "Anulowana" });
        }
    }
}
