using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class DronConfiguration : IEntityTypeConfiguration<Dron>
    {
        public void Configure(EntityTypeBuilder<Dron> builder)
        {
            builder.ToTable("Drony");

            builder.Property(x => x.Id);
            builder.Property(x => x.Producent).IsRequired();
            builder.Property(x => x.Model).IsRequired();
            builder.Property(x => x.NumerSeryjny).IsRequired();
            builder.Property(x => x.IdTypuDrona);
            builder.Property(x => x.DataNastepnegoPrzegladu);

            builder
                .HasOne(x => x.TypDrona)
                .WithMany(x => x.Drony)
                .HasForeignKey(x => x.IdTypuDrona);
        }
    }
}
