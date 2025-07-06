namespace AliyewShop.Domain.Entities;

public class OrderProduct : BaseEntity
{   
    public int ProductCount { get; set; }
    public decimal ProductPrice {  get; set; } 
    public Guid OrderId { get; set; }
    public Order Order { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
    public decimal PriceAtOrderTime { get; set; }
}
