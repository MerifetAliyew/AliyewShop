namespace AliyewShop.Application.DTOs.OrderProductDtos;

public class OrderProductCreateDto
{
    public Guid ProductId { get; set; }     // Məhsulun ID-si
    public int ProductCount { get; set; }   // Məhsul sayı
    public decimal ProductPrice { get; set; } // Məhsulun qiyməti (sifariş vaxtı qiymət)
}
