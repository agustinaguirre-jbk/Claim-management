namespace DeltaApi.Domain.Abstractions;

public abstract class Entity
{
    protected Entity(Guid id)
    {
        Id = id;
        CreatedOn = DateTime.UtcNow;
        Deleted = false;
    }

    protected Entity(Guid id, int createdByUser) : this(id)
    {
        CreatedByUser = createdByUser;
    }

    public Guid Id { get; init; }
    public bool Deleted { get; private set; }
    public int CreatedByUser { get; init; }
    public DateTime CreatedOn { get; init; }
    public int? ModifiedByUser { get; private set; }
    public DateTime? ModifiedOn { get; private set; }

    public void MarkAsDeleted(int deletedByUser)
    {
        Deleted = true;
        ModifiedByUser = deletedByUser;
        ModifiedOn = DateTime.UtcNow;
    }

    public void MarkAsModified(int modifiedByUser)
    {
        ModifiedByUser = modifiedByUser;
        ModifiedOn = DateTime.UtcNow;
    }

    public void Restore(int restoredByUser)
    {
        Deleted = false;
        ModifiedByUser = restoredByUser;
        ModifiedOn = DateTime.UtcNow;
    }
}