namespace Domain.Entities;

public class Entity
{
    public Guid Type { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedOn { get; set; } = null;
}