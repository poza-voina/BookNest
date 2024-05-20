using Domain.Entities;

namespace MusicApplication.Controllers.Dto.BookDto;

public class NewBookDto : BaseValidationDto
{
    // public int AuthorId { get; set; }
    [DtoValidation(required:true)]
    public string Title { get; set; } = default!;
    public DateOnly? PublishDate { get; set; }
    public string? Description { get; set; }
    public AuthorDto? Author { get; set; }
}