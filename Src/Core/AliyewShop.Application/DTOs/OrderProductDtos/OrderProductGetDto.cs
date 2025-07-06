namespace AliyewShop.Application.DTOs.OrderProductDtos;

public record class OrderProductGetDto
{
    public Guid Id { get; set; }
    public int ProductCount { get; set; }
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public string? ProductName { get; set; }
    public string? OrderNumber { get; set; }
}