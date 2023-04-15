using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GestoreBrani.Models;

// Tutte le classi create con il comando scaffold sono partial (vedere comincio lezione 12)
public partial class SongManagerContext : DbContext
{
    public SongManagerContext()
    {
    }

    public SongManagerContext(DbContextOptions<SongManagerContext> options)
        : base(options)
    {
    }

    public virtual DbSet<GenreTable> GenreTables { get; set; }

    public virtual DbSet<SongTable> SongTables { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code.
//You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148.
//For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Initial Catalog=song_manager;Persist Security Info=False;User ID=sa;Password=reallyStrongPwd123;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GenreTable>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("genre_PK");

            entity.ToTable("genre_table");

            entity.Property(e => e.GenreId)
                .ValueGeneratedNever()
                .HasColumnName("genre_Id");
            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .HasColumnName("description");
            entity.Property(e => e.GenreName)
                .HasMaxLength(50)
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<SongTable>(entity =>
        {
            entity.HasKey(e => e.SongId).HasName("song_PK");

            entity.ToTable("song_table");

            entity.Property(e => e.SongId)
                .ValueGeneratedNever()
                .HasColumnName("song_Id");
            entity.Property(e => e.AlbumTitle)
                .HasMaxLength(50)
                .HasColumnName("album_title");
            entity.Property(e => e.Author)
                .HasMaxLength(50)
                .HasColumnName("author");
            entity.Property(e => e.Genre).HasColumnName("genre");
            entity.Property(e => e.Interpreter)
                .HasMaxLength(50)
                .HasColumnName("interpreter");
            entity.Property(e => e.OccupiedSpace).HasColumnName("occupied_space");
            entity.Property(e => e.PublicationYear)
                .HasColumnType("ntext")
                .HasColumnName("publication_year");
            entity.Property(e => e.SongDuration).HasColumnName("song_duration");
            entity.Property(e => e.SongTitle)
                .HasMaxLength(50)
                .HasColumnName("song_title");

            // foreign key
            entity.HasOne(d => d.GenreNavigation).WithMany(p => p.SongTables)
                .HasForeignKey(d => d.Genre)
                .HasConstraintName("song_FK");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
