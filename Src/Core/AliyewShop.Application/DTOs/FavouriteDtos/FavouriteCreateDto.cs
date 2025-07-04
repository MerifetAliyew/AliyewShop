namespace AliyewShop.Application.DTOs.FavouriteDtos;

public record class FavouriteCreateDto
{
    public Guid ProductId { get; set; } // Hansı məhsul favori edilir
}
