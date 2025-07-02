namespace AliyewShop.Application.DTOs.OrderDtos;

public class OrderCreateDto
{
    public string ShippingAddress { get; set; } = null!;
    public string PaymentType { get; set; } = null!;
    public string? InternalNote { get; set; }
    public List<Guid> ProductIds { get; set; } = new();
}
