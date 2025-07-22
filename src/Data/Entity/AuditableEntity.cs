namespace Data.Common.Entity;

public abstract class AuditableEntity : ReadOnlyEntity
{
    public DateTime? UpdatedAtUtc { get; set; }
    public Guid? UpdatedBy { get; set; }

    public Guid? DeletedBy { get; set; }
    public DateTime? DeletedAtUtc { get; set; }

    public bool IsDeleted => DeletedAtUtc.HasValue;
}