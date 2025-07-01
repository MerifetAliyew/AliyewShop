namespace AliyewShop.Domain.Entities;

public class BaseEntity
{
    public Guid id { get; set; }
    public Guid? CreatedUser { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Guid? UpdateAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
