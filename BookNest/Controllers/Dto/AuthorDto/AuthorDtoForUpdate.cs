namespace MusicApplication.Controllers.Dto;

public class AuthorDtoForUpdate
{
    public string? Name { get; set; }
    public string? SecondName { get; set; }
    public string? Patronymic { get; set; }
    public DateOnly? BirthDate { get; set; }
}