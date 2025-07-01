namespace AliyewShop.Domain.Entities;
using System;
public class Image : BaseEntity
{
    public string ImageUrl { get; set; } = null!;
    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
