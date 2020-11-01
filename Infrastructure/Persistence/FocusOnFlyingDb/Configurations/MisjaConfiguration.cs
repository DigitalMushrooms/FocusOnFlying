using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class MisjaConfiguration : IEntityTypeConfiguration<Misja>
    {
        public void Configure(EntityTypeBuilder<Misja> builder)
        {
            builder.ToTable("Misje");

            builder.Property(x => x.Id);
            builder.Property(x => x.Nazwa).IsRequired();
            builder.Property(x => x.Opis).IsRequired();
            builder.Property(x => x.IdTypuMisji);
            builder.Property(x => x.MaksymalnaWysokoscLotu);
            builder.Property(x => x.IdStatusuMisji);
            builder.Property(x => x.DataRozpoczecia);
            builder.Property(x => x.DataZakonczenia);

            builder
                .HasOne(x => x.TypMisji)
                .WithOne(x => x.Misja)
                .HasForeignKey<Misja>(x => x.IdTypuMisji);

            builder
                .HasOne(x => x.StatusMisji)
                .WithOne(x => x.Misja)
                .HasForeignKey<Misja>(x => x.IdStatusuMisji);
        }
    }
}
