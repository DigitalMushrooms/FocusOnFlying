using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb
{
    public class FocusOnFlyingContext : DbContext, IFocusOnFlyingContext
    {
        private readonly IAppSettingsService _appSettingsService;

        public FocusOnFlyingContext(DbContextOptions options, IAppSettingsService appSettingsService) 
            : base(options)
        {
            _appSettingsService = appSettingsService;
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
        }
    }
}
