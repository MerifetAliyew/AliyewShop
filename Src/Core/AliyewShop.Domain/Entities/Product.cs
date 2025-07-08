namespace AliyewShop.Domain.Entities;

public class Product : BaseEntity
{

    // Məhsulun adı (məsələn: "Qadın Gödəkçəsi")
    public string Title { get; set; }

    // Məhsul haqqında açıqlama
    public string Description { get; set; }

    // Məhsulun qiyməti
    public decimal Price { get; set; }

    // Mövcud stok miqdarı
    public int StockCount { get; set; }

    // Məhsulun ölçüsü: S, M, L, XL
    public string Size { get; set; }

    // Məhsulun rəngi: "Qara", "Ağ", "Boz"
    public string Color { get; set; }

    // Məhsulun hansı cins üçün nəzərdə tutulduğunu göstərir
    public string Gender { get; set; } // "Kişi", "Qadın", "Uşaq" kimi

    // Məhsul hansı materialdandır (pambıq, dəridən və s.)
    public string Material { get; set; }

    // Məhsul hansı mövsüm üçün nəzərdə tutulub (qış, yay, yaz)
    public string Season { get; set; }

    // Məhsul hansı kateqoriyaya aiddir (məs: "Üst geyim", "Ayaqqabı")
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    // Məhsulu əlavə edən istifadəçi (satıcı)
    public string OwnerId { get; set; }
    public AppUser Owner { get; set; }

    public string SellerId { get; set; }   // 🔥 UserId kimi olacaq
    public AppUser Seller { get; set; }
    // Məhsula aid şəkillər
    public bool IsDeleted { get; set; } = false;
    public ICollection<Image> Images { get; set; }

    // Favoritə əlavə olunmuş məhsullar
    public ICollection<Favourite> Favourites { get; set; }

    // Məhsula verilən rəylər
    public ICollection<Review> Reviews { get; set; }

    // Sifarişlərlə olan əlaqə
    public ICollection<OrderProduct> OrderProducts { get; set; }
}
