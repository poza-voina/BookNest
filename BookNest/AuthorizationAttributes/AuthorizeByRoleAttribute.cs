using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
namespace BookNest.AuthorizationAttributes;


public class AuthorizeByRoleAttribute : AuthorizeAttribute
{
    public AuthorizeByRoleAttribute(string roles)
    {
        Roles = roles.ToString().Replace(" ", string.Empty);
    }
}