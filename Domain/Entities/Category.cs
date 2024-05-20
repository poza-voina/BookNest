using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Title { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
