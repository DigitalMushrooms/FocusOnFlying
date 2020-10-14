using FocusOnFlying.Application.Common.Interfaces;
using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb.Configurations;
using Microsoft.EntityFrameworkCore;

namespace FocusOnFlying.Infrastructure.Persistence.FocusOnFlyingDb
{
    public class FocusOnFlyingDbContext : DbContext
    {
        private readonly IAppSettingsService _appSettingsService;

        public FocusOnFlyingDbContext(DbContextOptions options, IAppSettingsService appSettingsService) 
            : base(options)
        {
            _appSettingsService = appSettingsService;
        }

        public DbSet<Klient> Klienci { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_appSettingsService.FocusOnFlyingConnectionString);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new KlientConfiguration());
        }
    }
}
