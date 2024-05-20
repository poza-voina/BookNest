using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Author : BaseEntity
{
    public string? Name { get; set; }

    public string? SecondName { get; set; }

    public string? Patronymic { get; set; }

    public DateOnly? BirthDate { get; set; }
    
    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
