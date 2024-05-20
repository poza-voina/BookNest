using System;
using System.Collections.Generic;

namespace Domain.Entities;
public class Book : BaseEntity
{
    
    public int? PublisherId { get; set; }

    public string? Title { get; set; }

    public DateOnly? PublishDate { get; set; }

    public string? Description { get; set; }

    public double Rate { get; set; } = 0;

    public int Ratings { get; set; } = 0;

    public virtual Author Author { get; set; } = null!;

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Publisher? Publisher { get; set; }

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
