using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WPFECZV1.Models;

public partial class Wpfeczv1Context : DbContext
{
    public Wpfeczv1Context()
    {
    }

    public Wpfeczv1Context(DbContextOptions<Wpfeczv1Context> options)
        : base(options)
    {
    }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=ZEMPIPC;Database=WPFECZV1;Trusted_Connection=True;TrustServerCertificate=True");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Books__3214EC07E7C98317");

            entity.Property(e => e.Article).HasMaxLength(50);
            entity.Property(e => e.Genre).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Reader).WithMany(p => p.Books)
                .HasForeignKey(d => d.ReaderId)
                .HasConstraintName("FK__Books__ReaderId__3D5E1FD2");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC078CBA92DC");

            entity.Property(e => e.AccessRights).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC0791C7FC4C");

            entity.HasIndex(e => e.Login, "UQ__Users__5E55825B9CC7C615").IsUnique();

            entity.Property(e => e.Login).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Surname).HasMaxLength(100);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Users__RoleId__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
