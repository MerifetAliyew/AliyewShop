namespace AliyewShop.Application.DTOs.OrderDtos;

public record class OrderCreateDto
{
    public List<Guid> ProductIds { get; set; } = new(); // Birdən çox məhsul sifariş edilə bilər
    public string? PaymentType { get; set; }
    public string? ShipToAddress { get; set; }
}