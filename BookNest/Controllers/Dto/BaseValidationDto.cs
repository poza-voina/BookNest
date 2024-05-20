using System.Reflection;

namespace MusicApplication.Controllers.Dto;

public interface IValidationDto
{
    public Dictionary<string, string>? Errors { get; set; }
    public bool IsValid();
}

public abstract class BaseValidationDto : IValidationDto
{
    public Dictionary<string, string>? Errors { get; set; }
    public virtual bool IsValid()
    {
        Errors = new Dictionary<string, string>();
        Type type = GetType();
        PropertyInfo[] properties = type.GetProperties();
        foreach (var property in properties)
        {
            DtoValidationAttribute? attribute = (DtoValidationAttribute)property.GetCustomAttribute(typeof(DtoValidationAttribute));
            if (attribute != null)
            {
                MethodInfo method = type.GetMethod(
                    attribute.ValidationFuncName,
                    BindingFlags.NonPublic | BindingFlags.Instance
                )!;
                if (property.GetValue(this) is null)
                {
                    if (attribute.Required)
                    {
                        Errors.Add(property.Name, attribute.ErrorMsg);
                    }
                }
                else if (attribute.ValidationFuncName != "" && (bool)method.Invoke(this, null)! == false)
                {
                    Errors.Add(property.Name, attribute.ErrorMsg);
                }
            }
        }

        return !Errors.Any();
    }
}