using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public class User : BaseUser
{
    [Column("Role")]
    public Role Role { get; set; } = Role.User;
}


[Flags]
public enum Role
{
    User = 1,
    Admin = 2,
}