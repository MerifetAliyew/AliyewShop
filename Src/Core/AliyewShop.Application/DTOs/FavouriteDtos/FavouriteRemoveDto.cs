namespace AliyewShop.Application.DTOs.FavouriteDtos;

public record class FavouriteRemoveDto
{
    public Guid ProductId { get; set; } // Hansı məhsul favoridən silinir
}
