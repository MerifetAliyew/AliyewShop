namespace AliyewShop.Application.DTOs.OrderDtos;

public class OrderGetDto
{
    public Guid Id { get; set; }
    public DateTime OrderAt { get; set; }
    public string? PaymentType { get; set; }
    public string? ShipToAddress { get; set; }
    public decimal? GrandTotal { get; set; }
    public string? Progress { get; set; }
}
