using System;
using System.Collections.Generic;

namespace Domain.Entities;

public class Comment : BaseEntity
{
    public int UserId { get; set; }

    public int BookId { get; set; }

    public string Text { get; set; } = null!;

    public DateOnly PublishDate { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
