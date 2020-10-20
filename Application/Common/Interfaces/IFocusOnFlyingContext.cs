using FocusOnFlying.Domain.Entities.FocusOnFlyingDb;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Common.Interfaces
{
    public interface IFocusOnFlyingContext
    {
        public DbSet<Klient> Klienci { get; set; }
        public DbSet<Usluga> Uslugi { get; set; }
        public DbSet<Kraj> Kraje { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
