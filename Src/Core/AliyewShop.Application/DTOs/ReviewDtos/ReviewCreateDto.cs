namespace AliyewShop.Application.DTOs.ReviewDtos;

public record class ReviewCreateDto
{
    public Guid ProductId { get; set; }
    public string? CommentBody { get; set; }
    public int Rating { get; set; }  // 1-dən 5-ə qədər qiymətləndirmə kimi nəzərdə tutula bilər
}
