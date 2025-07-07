namespace AliyewShop.Application.DTOs.ReviewDtos;

public record class ReviewCreateDto
{
    public string? CommentBody { get; set; }
    public int Rating { get; set; } // 1-5 arası dəyər
}
