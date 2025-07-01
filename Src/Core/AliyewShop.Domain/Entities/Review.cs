namespace AliyewShop.Domain.Entities;

public class Review : BaseEntity
{
    // Rəy moderator tərəfindən təsdiqlənib ya yox
    public bool IsConfirmed { get; set; }

    // Rəyin təsdiqləndiyi tarix
    public DateTime? ConfirmedAt { get; set; }

    // Rəy hansı məhsula aiddir
    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    // Rəyi yazan istifadəçi
    public string UserId { get; set; }
    public User User { get; set; }

    // Rəyin mətni
    public string? CommentBody { get; set; } // əvvəl: Content

    // Rəyin yazıldığı vaxt
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }

}

