using BookNest.AuthorizationAttributes;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Repositories;
using MusicApplication.Controllers.Dto.BookDto;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using MusicApplication.Controllers.Dto;

namespace MusicApplication.Controllers;

[Route("books")]
public class BookController : ControllerBase
{
    private readonly IBookRepository _bookRepository;
    private readonly IAuthorRepository _authorRepository;
    
    public BookController(IBookRepository bookRepository, IAuthorRepository authorRepository)
    {
        _bookRepository = bookRepository;
        _authorRepository = authorRepository;
    }

    [AuthorizeByRole("Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateBook([FromBody] NewBookDto bookDto)
    {
        if (!bookDto.IsValid())
        {
            return BadRequest(bookDto.Errors);
        }

        AuthorDto? authorDto = bookDto.Author;
        if (bookDto.Author != null && !bookDto.Author.IsValid())
        {
            return BadRequest(bookDto.Author.Errors);
        }
        Console.WriteLine(authorDto.Name);

        
        Author? author = await _authorRepository.TryGetByDataAsync(authorDto!.Name, authorDto.SecondName, authorDto.Patronymic, authorDto.BirthDate);
        if (author == null)
        {  
            author = new Author{Name = authorDto!.Name, SecondName = authorDto.SecondName, Patronymic = authorDto.Patronymic, BirthDate = authorDto.BirthDate};
        }
        

        Book book = new Book { Title = bookDto.Title, Author = author };
        if (await _bookRepository.IsBookAlreadyExistAsync(book))
        {
            return Conflict();
        }

        await _bookRepository.CreateAsync(book);
        return Ok();
    }
    
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById([FromRoute]int id)
    {
        Book book;
        try
        {
            book = await _bookRepository.GetAsync(id);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine(ex);
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        return Ok(_bookRepository.Items.ToList());
    }

    [AuthorizeByRole("Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBookById([FromRoute] int id)
    {
        Book book;
        try
        {
            book = await _bookRepository.GetAsync(id);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        await _bookRepository.DeleteAsync(book);
        return Ok();
    }
    
    [AuthorizeByRole("Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBookById([FromRoute] int id, [FromBody] BookDtoForUpdate bookDto)
    {
        Book book;
        try
        {
            book = await _bookRepository.GetAsync(id);
        }
        catch (ArgumentException ex)
        {
            return NotFound();
        }

        
        book.Title = bookDto.Title ?? book.Title;
        book.Description = bookDto.Title ?? book.Title;
        if (bookDto.Author != null)
        {
            var authorDto = bookDto.Author;
            book.Author = new Author
                { Name = authorDto.Name, SecondName = authorDto.SecondName, BirthDate = authorDto.BirthDate };
        }

        await _bookRepository.UpdateAsync(book);
        return Ok();
    }
    
}