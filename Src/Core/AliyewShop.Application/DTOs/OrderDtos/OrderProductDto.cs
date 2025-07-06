namespace AliyewShop.Application.DTOs.OrderDtos;

public record class OrderProductDto
{
    public Guid ProductId { get; set; }
    public string ProductTitle { get; set; } = null!;
    public decimal ProductPrice { get; set; }
}