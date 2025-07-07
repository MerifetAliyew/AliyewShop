using System.Net;
using System.Security.Claims;
using AliyewShop.Application.Abstracts.Repositories;
using AliyewShop.Application.Abstracts.Services;
using AliyewShop.Application.DTOs.ReviewDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AliyewShop.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReviewsController : ControllerBase
{
    private readonly IReviewService _reviewService;
    private readonly IReviewRepository _reviewRepository;

    public ReviewsController(IReviewService reviewService, IReviewRepository reviewRepository)
    {
        _reviewService = reviewService;
        _reviewRepository = reviewRepository;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReview(Guid productId, [FromBody] ReviewCreateDto dto)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userId == null)
            return Unauthorized();

        var response = await _reviewService.CreateReviewAsync(productId, dto, userId);

        if (response.StatusCode == HttpStatusCode.Created)
            return CreatedAtAction(nameof(GetReviews), new { productId = productId }, response.Message);

        return BadRequest(response.Message);
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews(Guid productId)
    {
        var response = await _reviewService.GetReviewsByProductIdAsync(productId);

        if (response.StatusCode == HttpStatusCode.OK)
            return Ok(response.Data);

        return NotFound(response.Message);
    }

    // Admin təsdiqi üçün endpoint
    [HttpPut("{reviewId}/confirm")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ConfirmReview(Guid productId, Guid reviewId)
    {
        var review = await _reviewRepository.GetByIdAsync(reviewId);
        if (review == null || review.ProductId != productId)
            return NotFound(new { message = "Review tapılmadı" });

        if (review.IsConfirmed)
            return BadRequest(new { message = "Review artıq təsdiqlənib" });

        review.IsConfirmed = true;
        review.ConfirmedAt = DateTime.UtcNow;

        _reviewRepository.Update(review);
        await _reviewRepository.SaveChangeAsync();

        return Ok(new { message = "Review təsdiqləndi" });
    }
}
