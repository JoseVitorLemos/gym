namespace Gym.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; protected set; }
    public bool Status { get; protected set; }

    public BaseEntity()
    {
        Id = Guid.NewGuid();
        Status = true;
    }

    public void AlterStatus(bool status = false)
        => Status = status;
}
