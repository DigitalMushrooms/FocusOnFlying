using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class UslugaConfiguration : IEntityTypeConfiguration<Usluga>
    {
        public void Configure(EntityTypeBuilder<Usluga> builder)
        {
            builder.ToTable("Uslugi");
            builder
                .HasOne(x => x.Klient)
                .WithMany(x => x.Uslugi)
                .HasForeignKey(x => x.IdKlienta);

            builder
                .HasOne(x => x.StatusUslugi)
                .WithMany(x => x.Uslugi)
                .HasForeignKey(x => x.IdStatusuUslugi);

            builder
                .HasOne(x => x.Faktura)
                .WithOne(x => x.Usluga)
                .HasForeignKey<Usluga>(x => x.IdFaktury);
        }
    }
}
