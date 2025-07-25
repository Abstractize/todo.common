using Microsoft.EntityFrameworkCore;
using Data.Common.Entity;
using Services.Common.Identity;
using System.Linq.Expressions;

namespace Data.Common.Context
{
    public abstract class BaseContext<TDatabaseContext>(
        IIdentityService identityService,
        DbContextOptions<TDatabaseContext> options
    ) : DbContext(options)
    where TDatabaseContext : BaseContext<TDatabaseContext>
    {
        private readonly IIdentityService _identityService = identityService;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(AuditableEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var prop = Expression.PropertyOrField(parameter, nameof(AuditableEntity.DeletedAtUtc));
                    var condition = Expression.Equal(prop, Expression.Constant(null));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }
        }

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
