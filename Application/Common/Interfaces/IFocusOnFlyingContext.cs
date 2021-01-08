using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface IFocusOnFlyingContext
    {
        DbSet<Klient> Klienci { get; set; }
        DbSet<Usluga> Uslugi { get; set; }
        DbSet<Kraj> Kraje { get; set; }
        DbSet<Misja> Misje { get; set; }
        DbSet<TypMisji> TypyMisji { get; set; }
        DbSet<StatusMisji> StatusyMisji { get; set; }
        DbSet<StatusUslugi> StatusyUslugi { get; set; }
        DbSet<TypDrona> TypyDrona { get; set; }
        DbSet<Dron> Drony { get; set; }
        DbSet<MisjaDron> MisjeDrony { get; set; }
        DbSet<Faktura> Faktury { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        ChangeTracker ChangeTracker { get; }
    }
}
