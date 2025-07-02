namespace AliyewShop.Application.DTOs.ReviewDtos;

public class ReviewGetDto
{
    public Guid Id { get; set; }
    public string? CommentBody { get; set; }
    public int Rating { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string UserId { get; set; }
    public string UserName { get; set; } = null!;  // Rəyi yazan istifadəçinin adı və ya username
}
