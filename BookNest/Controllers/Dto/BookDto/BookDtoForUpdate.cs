namespace MusicApplication.Controllers.Dto.BookDto;

public class BookDtoForUpdate
{
    public string? Title { get; set; }
    public DateOnly? PublishDate { get; set; }
    public string? Description { get; set; }
    public AuthorDto? Author { get; set; }
}