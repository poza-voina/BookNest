using System.Security.Claims;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicApplication.Controllers.Dto.ReviewDto;

namespace MusicApplication.Controllers;

[Route("reviews")]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    
    public ReviewController(IReviewRepository reviewRepository, IUserRepository userRepository, IBookRepository bookRepository)
    {
        _reviewRepository = reviewRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> CreateReview([FromBody] ReviewDtoForCreate reviewDto)
    {
        if (!reviewDto.IsValid())
        {
            return BadRequest();
        }

        Book book;
        try
        {
            book = await _bookRepository.GetAsync(reviewDto.BookId);
        }
        catch (ArgumentException ex)
        {
            return NotFound();
        }
        string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
        book.Ratings++;
        book.Rate = (book.Rate * book.Ratings + reviewDto.Score) / (book.Ratings + 1);
        Review review = new Review { Text = reviewDto.Text, Score = reviewDto.Score, User = (await _userRepository.TryGetByEmailAsync(email))!, Book = book};
        await _reviewRepository.CreateAsync(review);
        return Ok();
    }
}