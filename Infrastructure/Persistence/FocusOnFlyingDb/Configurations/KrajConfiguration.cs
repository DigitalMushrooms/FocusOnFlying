using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations
{
    public class KrajConfiguration : IEntityTypeConfiguration<Kraj>
    {
        public void Configure(EntityTypeBuilder<Kraj> builder)
        {
            builder.ToTable("Kraje");
            builder.Property(p => p.NazwaKraju).HasMaxLength(50).IsRequired();
            builder.HasIndex(x => x.NazwaKraju).IsUnique();
            builder.Property(p => p.Skrot).HasMaxLength(2).IsRequired();
            builder.HasIndex(x => x.Skrot).IsUnique();

            builder.HasData(
                new Kraj { Id = new Guid("abaccb22-4d71-41ef-b96b-199fb007d336"), NazwaKraju = "Polska", Skrot = "PL" },
                new Kraj { Id = new Guid("ccf2c1c3-56ab-4e12-924c-0e9996ff261f"), NazwaKraju = "Niemcy", Skrot = "DE" },
                new Kraj { Id = new Guid("d356b661-b7d1-4c75-80c7-677062dfdb94"), NazwaKraju = "Czechy", Skrot = "CZ" },
                new Kraj { Id = new Guid("2f314b76-9fd4-4e89-ad94-0a4cc9a92f18"), NazwaKraju = "Słowacja", Skrot = "SK" },
                new Kraj { Id = new Guid("1863424c-f0ee-40e8-b8b7-683a445a8d6e"), NazwaKraju = "Ukraina", Skrot = "UA" },
                new Kraj { Id = new Guid("be2f6802-38bc-4ac3-abc2-d19714e6689d"), NazwaKraju = "Białoruś", Skrot = "BY" },
                new Kraj { Id = new Guid("0847f9ab-bd5a-4714-93cb-5a4ec23afee2"), NazwaKraju = "Litwa", Skrot = "LV" },
                new Kraj { Id = new Guid("b57f5888-24ce-4349-8de8-dcb938678915"), NazwaKraju = "Rosja", Skrot = "RU" }
            );
        }
    }
}
