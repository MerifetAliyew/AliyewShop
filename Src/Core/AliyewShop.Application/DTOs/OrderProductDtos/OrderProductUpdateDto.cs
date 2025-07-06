namespace AliyewShop.Application.DTOs.OrderProductDtos;

public record class OrderProductUpdateDto
{
    public Guid Id { get; set; }  // Yeniləmək üçün ID lazımdır
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
}