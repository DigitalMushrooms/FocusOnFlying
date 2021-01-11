using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class AudytConfiguration : IEntityTypeConfiguration<Audyt>
    {
        public void Configure(EntityTypeBuilder<Audyt> builder)
        {
            builder.Property(p => p.Id);
            builder.Property(p => p.IdAudytowanegoWiersza);
            builder.Property(p => p.NazwaTabeli).IsRequired();
            builder.Property(p => p.Dane).IsRequired();
            builder.Property(p => p.DataAudytu);
            builder.Property(p => p.Uzytkownik).IsRequired();
            builder.Property(p => p.TypOperacji).IsRequired();
        }
    }
}
