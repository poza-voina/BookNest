using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Domain.Entities;

public class BaseUser : BaseEntity
{
    [Column("Username")]
    public string Username { get; set; } = default!;

    [Column("Email")]
    public string Email { get; set; } = default!;
    
    [JsonIgnore]
    [Column("PasswordHash")]
    public string PasswordHash { get; set; } = default!;
    [JsonIgnore]
    [Column("PasswordSalt")]
    public byte[] PasswordSalt { get; set; } = default!;
    [JsonIgnore]
    [Column("LastLoginDate")]
    public DateTime LastLoginDate { get; set; } = default!;
}

