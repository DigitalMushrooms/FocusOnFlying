using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    class KlientConfiguration : IEntityTypeConfiguration<Klient>
    {
        public void Configure(EntityTypeBuilder<Klient> builder)
        {
            builder.ToTable("Klienci");
            builder.Property(p => p.Imie).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Nazwisko).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Pesel).HasMaxLength(11);
            builder.Property(p => p.Regon).HasMaxLength(14);
            builder.Property(p => p.Nip).HasMaxLength(10);
            builder.Property(p => p.NumerPaszportu).HasMaxLength(50);
            builder.Property(p => p.NumerTelefonu).IsRequired().HasMaxLength(20);
            builder.Property(p => p.KodPocztowy).HasMaxLength(20);
            builder.Property(p => p.Miejscowosc).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Ulica).IsRequired().HasMaxLength(100);
            builder.Property(p => p.NumerDomu).IsRequired().HasMaxLength(10);
            builder.Property(p => p.NumerLokalu).IsRequired().HasMaxLength(10);
            builder.Property(p => p.SymbolPanstwa).IsRequired().HasMaxLength(3);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(256);
        }
    }
}
