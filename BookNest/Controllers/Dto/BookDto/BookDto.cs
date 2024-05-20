namespace MusicApplication.Controllers.Dto.BookDto;

public class BookDto
{
    public int? PublisherId { get; set; }
    public string? Title { get; set; }
    public DateOnly? PublishDate { get; set; }
    public string? Description { get; set; }
    public double? Rate { get; set; }
    public int? Ratings { get; set; }
    public AuthorDto? Author { get; set; }
}