using System.Text.RegularExpressions;
using Infrastructure.Services;

namespace MusicApplication.Controllers.Dto;

public class RegistrationDto : BaseValidationDto
{
    [DtoValidation(validationFuncName: nameof(ValidateEmail), required: true, errorMsg: "Email no valid")]
    public string? Email { get; set; }
    [DtoValidation(required: true, errorMsg: "Username no valid")]
    public string? Username { get; set; }
    [DtoValidation(validationFuncName: nameof(ValidatePassword), required: true, errorMsg: "Password no valid")]
    public string? Password { get; set; }

    private static readonly string _allowedPasswordChars = PasswordManager.AllowedPasswordChars;
    protected bool ValidateEmail()
    {   
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        Regex regex = new Regex(pattern);
        return regex.IsMatch(Email!);
    }

    protected bool ValidatePassword()
    {
        foreach (char ch in Password!)
        {
            if (!_allowedPasswordChars.Contains(ch))
            {
                return false;
            }
        }

        return true;
    }
}