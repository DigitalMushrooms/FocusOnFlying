using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class TypMisjiConfiguration : IEntityTypeConfiguration<TypMisji>
    {
        public void Configure(EntityTypeBuilder<TypMisji> builder)
        {
            builder.ToTable("TypyMisji");
            builder.Property(p => p.Nazwa).IsRequired();

            builder.HasData(new TypMisji { Id = new Guid("ed9994f8-c9c8-4781-8423-67bcf005064f"), Nazwa = "BVLOS" });
            builder.HasData(new TypMisji { Id = new Guid("f9a094e4-c4ae-492d-9af4-966022b156d9"), Nazwa = "VLOS" });
        }
    }
}
