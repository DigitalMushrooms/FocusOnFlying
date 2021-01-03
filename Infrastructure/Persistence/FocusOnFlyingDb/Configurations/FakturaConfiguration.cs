using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class FakturaConfiguration : IEntityTypeConfiguration<Faktura>
    {
        public void Configure(EntityTypeBuilder<Faktura> builder)
        {
            builder.ToTable("Faktury");

            builder.Property(p => p.Id);
            builder.Property(p => p.NumerFaktury).IsRequired();
            builder.Property(p => p.WartoscNetto).HasColumnType("decimal(19,4)");
            builder.Property(p => p.WartoscBrutto).HasColumnType("decimal(19,4)");
            builder.Property(p => p.ZaplaconaFaktura);
        }
    }
}
