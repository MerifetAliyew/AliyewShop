namespace AliyewShop.Application.DTOs.ReviewDtos;

public record ReviewGetDto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string UserId { get; set; }
    public string UserFullName { get; set; } 
    public string? CommentBody { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedAt { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; }
}
