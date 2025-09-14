using System;
using System.Collections.Generic;

namespace WPFECZV1.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Article { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly PublicationDate { get; set; }

    public int State { get; set; }

    public int? ReaderId { get; set; }

    public virtual User? Reader { get; set; }
}
