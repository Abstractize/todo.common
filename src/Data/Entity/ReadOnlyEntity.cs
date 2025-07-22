namespace Data.Common.Entity;

public abstract class ReadOnlyEntity : BaseEntity<Guid>
{
    public DateTime CreatedAtUtc { get; set; }
    public Guid CreatedBy { get; set; }
}
