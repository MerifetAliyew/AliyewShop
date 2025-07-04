namespace AliyewShop.Application.DTOs.ReviewDtos;

public record class ReviewUpdateDto
{
    public Guid Id { get; set; }  // Yenilənəcək rəyin ID-si
    public string? CommentBody { get; set; }
    public int Rating { get; set; }
}
