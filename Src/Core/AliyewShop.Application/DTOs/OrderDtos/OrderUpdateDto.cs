namespace AliyewShop.Application.DTOs.OrderDtos;

public record class OrderUpdateDto
{
    public string? Progress { get; set; }
    public string? InternalNote { get; set; }
}
