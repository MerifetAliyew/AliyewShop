namespace AliyewShop.Application.DTOs.OrderDtos;

public record class OrderCreateDto
{
    public string UserId { get; set; }
    public string ShippingAddress { get; set; } = null!;
    public string PaymentType { get; set; } = null!;
    public string? InternalNote { get; set; }
    public List<Guid> ProductIds { get; set; } = new();
}
