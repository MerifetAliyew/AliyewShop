using System.ComponentModel.DataAnnotations.Schema;

namespace AliyewShop.Domain.Entities;

public class Order : BaseEntity
{
    // Sifarişin verildiyi tarix
    public DateTime OrderAt { get; set; }  

    // Ödəniş üsulu: məsələn "Nağd", "Kartla"
    public string? PaymentType { get; set; } 

    // Məhsulun çatdırılacağı ünvan
    public string? ShipToAddress { get; set; } 

    // Sifarişin ümumi qiyməti (toplam məbləğ)
    public decimal? GrandTotal { get; set; } 

    // Sifarişin vəziyyəti: "Pending", "Completed", "Cancelled"
    public string? Progress { get; set; } 

    // Alıcının və ya adminin sifarişə əlavə etdiyi qeyd
    public string? InternalNote { get; set; } 
    public string UserId { get; set; }
    public AppUser User { get; set; }

    // Bu sifarişdə hansı məhsullar var
    public ICollection<OrderProduct> OrderDetails { get; set; }
}
