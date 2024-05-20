namespace MusicApplication.Controllers.Dto;

public class LoginDto : BaseValidationDto
{
    [DtoValidation(required: true)]
    public string? Email { get; set; }
    [DtoValidation(required:true)]
    public string? Password { get; set; }
    
}