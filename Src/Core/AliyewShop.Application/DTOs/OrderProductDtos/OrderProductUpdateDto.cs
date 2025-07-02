namespace AliyewShop.Application.DTOs.OrderProductDtos;

public class OrderProductUpdateDto
{
    public Guid Id { get; set; }             // OrderProduct-un ID-si
    public int ProductCount { get; set; }
    public decimal ProductPrice { get; set; }
}