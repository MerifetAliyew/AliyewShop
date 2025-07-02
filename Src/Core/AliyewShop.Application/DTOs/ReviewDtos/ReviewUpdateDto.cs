namespace AliyewShop.Application.DTOs.ReviewDtos;

public class ReviewUpdateDto
{
    public Guid Id { get; set; }  // Yenilənəcək rəyin ID-si
    public string? CommentBody { get; set; }
    public int Rating { get; set; }
}
