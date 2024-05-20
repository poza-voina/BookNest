namespace MusicApplication.Controllers.Dto;

public class AuthorDto : BaseValidationDto
{
    [DtoValidation(required: true)]
    public string Name { get; set; } = default!;

    [DtoValidation(required: true)]
    public string SecondName { get; set; } = default!;
    [DtoValidation(required: false)]
    public string? Patronymic { get; set; }
    [DtoValidation(required: false)]
    public DateOnly? BirthDate { get; set; }
}