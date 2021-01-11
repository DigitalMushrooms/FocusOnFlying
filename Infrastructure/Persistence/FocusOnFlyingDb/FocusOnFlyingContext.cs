using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using FocusOnFlying.Domain.Interfaces;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb
{
    public class FocusOnFlyingContext : DbContext, IFocusOnFlyingContext
    {
        private readonly IAppSettingsService _appSettingsService;
        private readonly ICurrentUserService _currentUserService;

        public FocusOnFlyingContext(DbContextOptions options,
                                    IAppSettingsService appSettingsService,
                                    ICurrentUserService currentUserService)
            : base(options)
        {
            _appSettingsService = appSettingsService;
            _currentUserService = currentUserService;
        }

        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Usluga> Uslugi { get; set; }
        public DbSet<Kraj> Kraje { get; set; }
        public DbSet<Misja> Misje { get; set; }
        public DbSet<TypMisji> TypyMisji { get; set; }
        public DbSet<StatusMisji> StatusyMisji { get; set; }
        public DbSet<StatusUslugi> StatusyUslugi { get; set; }
        public DbSet<TypDrona> TypyDrona { get; set; }
        public DbSet<Dron> Drony { get; set; }
        public DbSet<MisjaDron> MisjeDrony { get; set; }
        public DbSet<Faktura> Faktury { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettingsService.FocusOnFlyingConnectionString);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var wierszeAudytowe = new List<Audyt>();
            var obecnaChwila = DateTime.Now;
            foreach (EntityEntry<IAudytowalnaTabela> entry in ChangeTracker.Entries<IAudytowalnaTabela>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        wierszeAudytowe.Add(UtworzAudyt(entry, obecnaChwila));
                        break;
                }
            }

            if (wierszeAudytowe.Any())
            {
                AddRange(wierszeAudytowe);
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        private Audyt UtworzAudyt(EntityEntry<IAudytowalnaTabela> entry, DateTime obecnaChwila)
        {
            PropertyEntry idAudytowanegoWiersza = null;
            if (entry.Properties.Any(x => x.Metadata.Name == "Id"))
            {
                idAudytowanegoWiersza = entry.Property("Id");
            }
            Type typKlasy = entry.Entity.GetType();
            IEntityType typEntity = Model.FindEntityType(typKlasy);

            var wierszAudytu = new Audyt();
            wierszAudytu.IdAudytowanegoWiersza = idAudytowanegoWiersza == null ? Guid.Empty : (Guid)idAudytowanegoWiersza.OriginalValue;
            wierszAudytu.NazwaTabeli = typEntity.GetTableName();
            wierszAudytu.Dane = JsonConvert.SerializeObject(entry.Entity);
            wierszAudytu.DataAudytu = obecnaChwila;
            wierszAudytu.Uzytkownik = _currentUserService.Id;
            wierszAudytu.TypOperacji = entry.State.ToString();

            return wierszAudytu;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KlientConfiguration());
            modelBuilder.ApplyConfiguration(new UslugaConfiguration());
            modelBuilder.ApplyConfiguration(new KrajConfiguration());
            modelBuilder.ApplyConfiguration(new MisjaConfiguration());
            modelBuilder.ApplyConfiguration(new TypMisjiConfiguration());
            modelBuilder.ApplyConfiguration(new StatusMisjiConfiguration());
            modelBuilder.ApplyConfiguration(new StatusUslugiConfiguration());
            modelBuilder.ApplyConfiguration(new TypDronaConfiguration());
            modelBuilder.ApplyConfiguration(new DronConfiguration());
            modelBuilder.ApplyConfiguration(new MisjaDronConfiguration());
            modelBuilder.ApplyConfiguration(new FakturaConfiguration());
            modelBuilder.ApplyConfiguration(new AudytConfiguration());
        }
    }
}
