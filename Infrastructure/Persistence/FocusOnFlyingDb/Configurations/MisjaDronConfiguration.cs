using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    class MisjaDronConfiguration : IEntityTypeConfiguration<MisjaDron>
    {
        public void Configure(EntityTypeBuilder<MisjaDron> builder)
        {
            builder.ToTable("MisjeDrony");

            builder.HasKey(x => new { x.IdMisji, x.IdDrona });

            builder
                .HasOne(x => x.Misja)
                .WithMany(x => x.MisjeDrony)
                .HasForeignKey(x => x.IdMisji);

            builder
                .HasOne(x => x.Dron)
                .WithMany(x => x.MisjeDrony)
                .HasForeignKey(x => x.IdDrona);
        }
    }
}
