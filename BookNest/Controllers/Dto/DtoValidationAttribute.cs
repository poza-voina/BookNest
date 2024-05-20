namespace MusicApplication.Controllers.Dto;



public class DtoValidationAttribute : Attribute
{
    public string ValidationFuncName { get; init; }
    public bool Required { get; init; }
    public string ErrorMsg { get; init; }

    public DtoValidationAttribute(string? validationFuncName = null, bool required = false, string? errorMsg = null) : base()
    {
        ValidationFuncName = validationFuncName ?? "";
        Required = required;
        ErrorMsg = errorMsg ?? "Missing error Msg";
    }
    
}