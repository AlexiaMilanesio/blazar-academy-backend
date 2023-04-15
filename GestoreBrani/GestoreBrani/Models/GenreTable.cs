using System;
using System.Collections.Generic;

namespace GestoreBrani.Models;

public partial class GenreTable
{
    public Guid GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<SongTable> SongTables { get; set; } = new List<SongTable>(); 
}
