using System;
using System.Collections.Generic;

namespace GestoreBrani.Models;

public partial class SongTable
{
    public string SongId { get; set; }

    public string SongTitle { get; set; } = null!;

    public string? AlbumTitle { get; set; }

    public string Author { get; set; } = null!;

    public string Interpreter { get; set; } = null!;

    public Guid? Genre { get; set; }

    public string PublicationYear { get; set; } = null!;

    public int SongDuration { get; set; }

    public int OccupiedSpace { get; set; }

    // navigate from one point to the other of foreign key
    public virtual GenreTable? GenreNavigation { get; set; }
}
