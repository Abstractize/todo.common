using Microsoft.EntityFrameworkCore;
using Data.Common.Entity;
using Services.Common.Identity;

namespace Data.Common.Context
{
    public abstract class BaseContext<TDatabaseContext>(
        IIdentityService identityService,
        DbContextOptions<TDatabaseContext> options
    ) : DbContext(options)
    where TDatabaseContext : BaseContext<TDatabaseContext>
    {
        private readonly IIdentityService _identityService = identityService;

        public override int SaveChanges()
        {
            ApplyAuditing();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ApplyAuditing();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void ApplyAuditing()
        {
            DateTime now = DateTime.UtcNow;
            Guid userId = _identityService.UserId ?? Guid.Empty;

            foreach (var entry in ChangeTracker.Entries<ReadOnlyEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedAtUtc = now;
                        entry.Entity.CreatedBy = userId;
                        break;
                    case EntityState.Modified:
                        // ReadOnlyEntity should not be modified
                        break;
                }
            }


            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.UpdatedAtUtc = now;
                        entry.Entity.UpdatedBy = userId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdatedAtUtc = now;
                        entry.Entity.UpdatedBy = userId;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.Entity.DeletedAtUtc = now;
                        entry.Entity.DeletedBy = userId;
                        break;
                }
            }
        }
    }
}
