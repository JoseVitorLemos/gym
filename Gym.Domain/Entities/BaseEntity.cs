namespace Gym.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public bool Status { get; protected set; }
    public DateTime CreatedAt { get; protected set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        Status = true;
        CreatedAt = DateTime.UtcNow;
    }

    public void AlterStatus(bool status = false)
        => Status = status;
}
