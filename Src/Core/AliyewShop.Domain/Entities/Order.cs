namespace AliyewShop.Domain.Entities;

public class Order : BaseEntity
{
    // Sifarişin verildiyi tarix
    public DateTime OrderAt { get; set; }  // əvvəl: OrderDate

    // Ödəniş üsulu: məsələn "Nağd", "Kartla"
    public string? PaymentType { get; set; } // əvvəl: PaymentMethod

    // Məhsulun çatdırılacağı ünvan
    public string? ShipToAddress { get; set; } // əvvəl: DeliveryAddress

    // Sifarişin ümumi qiyməti (toplam məbləğ)
    public decimal? GrandTotal { get; set; } // əvvəl: TotalPrice

    // Sifarişin vəziyyəti: "Pending", "Completed", "Cancelled"
    public string? Progress { get; set; } // əvvəl: Status

    // Alıcının və ya adminin sifarişə əlavə etdiyi qeyd
    public string? InternalNote { get; set; } // əvvəl: Note

    // Sifarişlə əlaqəli istifadəçilər (Buyer, Seller və ya Admin ola bilər)
    public ICollection<User> LinkedUsers { get; set; } // əvvəl: AppUsers

    // Bu sifarişdə hansı məhsullar var
    public ICollection<OrderProduct> OrderDetails { get; set; } // əvvəl: OrderProducts
}
