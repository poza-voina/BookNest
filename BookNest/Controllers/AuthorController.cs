using System.Security.Claims;
using BookNest.AuthorizationAttributes;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicApplication.Controllers.Dto;

namespace MusicApplication.Controllers;

[Route("authors")]
public class AuthorController : ControllerBase
{
    private readonly IAuthorRepository _authorRepository;

    public AuthorController(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    [AuthorizeByRole("Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorDto authorDto)
    {
        if (!authorDto.IsValid())
        {
            return BadRequest(authorDto.Errors);
        }

        Author author = new Author
        {
            Name = authorDto.Name, SecondName = authorDto.SecondName, Patronymic = authorDto.Patronymic,
            BirthDate = authorDto.BirthDate
        };
        if (await _authorRepository.IsAuthorAlreadyExistAsync(author))
        {
            return Conflict();
        }

        await _authorRepository.CreateAsync(author);
        return Ok();
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetAuthorById([FromRoute] int id)
    {
        Author author;
        try
        {
            author = await _authorRepository.GetAsync(id);
        }
        catch (ArgumentException ex)
        {
            return NotFound();
        }

        return Ok(new AuthorDto
        {
            Name = author.Name, SecondName = author.SecondName, Patronymic = author.Patronymic,
            BirthDate = author.BirthDate
        });
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAuthors()
    {
        var authors = _authorRepository.Items.ToList();
        return Ok(authors);
    }
    
    [AuthorizeByRole("Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthor([FromRoute] int id)
    {
        Author author;
        try
        {
            author = await _authorRepository.GetAsync(id);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }
        await _authorRepository.DeleteAsync(author);
        return Ok();
    }
    
    [AuthorizeByRole("Admin")]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAuthor([FromRoute] int id, [FromBody] AuthorDtoForUpdate authorDto)
    {
        Author author;
        try
        {
            author = await _authorRepository.GetAsync(id);
        }
        catch (ArgumentException)
        {
            return NotFound();
        }

        author.Name = authorDto.Name ?? author.Name;
        author.SecondName = authorDto.SecondName ?? author.SecondName;
        author.Patronymic = authorDto.Patronymic ?? author.Patronymic;
        author.BirthDate = authorDto.BirthDate ?? author.BirthDate;
        await _authorRepository.UpdateAsync(author);
        return Ok();
    }
}