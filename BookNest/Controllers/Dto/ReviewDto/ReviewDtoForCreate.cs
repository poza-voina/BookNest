namespace MusicApplication.Controllers.Dto.ReviewDto;

public class ReviewDtoForCreate : BaseValidationDto
{
    [DtoValidation(required: true, errorMsg: "BookId is required")]
    public int BookId { get; set; } = default!;

    [DtoValidation(required: true, errorMsg: "Text is required")]
    public string Text { get; set; } = default!;

    [DtoValidation(validationFuncName: nameof(ValidateScore), required: true, errorMsg: "Score incorrect")]
    public double Score { get; set; } = default!;


    protected bool ValidateScore()
    {
        return 0 <= Score && Score <= 10;
    }
    

}