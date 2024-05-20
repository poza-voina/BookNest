using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Domain.Entities;

public class Review : BaseEntity
{
    public string Text { get; set; } = default!;
    public double Score { get; set; }

    [JsonIgnore]
    public virtual Book Book { get; set; } = null!;
    public virtual User User { get; set; } = null!;
}
