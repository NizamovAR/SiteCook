using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SiteCook.Models;

public partial class CookingBookContext : DbContext
{
    public CookingBookContext()
    {
    }

    public CookingBookContext(DbContextOptions<CookingBookContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<Meal> Meals { get; set; }

    public virtual DbSet<Moderator> Moderators { get; set; }

    public virtual DbSet<Recipe> Recipes { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=CookingBook;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.IdAdministrator).HasName("PK__Administ__27833EB966863BC6");

            entity.ToTable("Administrator");

            entity.Property(e => e.Mail).HasMaxLength(50);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__Category__CBD747060F501BC3");

            entity.ToTable("Category");

            entity.Property(e => e.ImageCategory).HasColumnType("image");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasKey(e => e.IdComment).HasName("PK__Comment__57C9AD588F898345");

            entity.ToTable("Comment");

            entity.Property(e => e.DateComement).HasColumnType("datetime");

            entity.HasOne(d => d.IdRecipeNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdRecipe)
                .HasConstraintName("FK__Comment__IdRecip__47DBAE45");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Comments)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Comment__IdUser__46E78A0C");
        });

        modelBuilder.Entity<Meal>(entity =>
        {
            entity.HasKey(e => e.IdMeal).HasName("PK__Meal__4D7C3B3A5791D8E2");

            entity.ToTable("Meal");

            entity.Property(e => e.ImageMeal).HasColumnType("image");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Meals)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK__Meal__IdCategory__38996AB5");
        });

        modelBuilder.Entity<Moderator>(entity =>
        {
            entity.HasKey(e => e.IdModerator).HasName("PK__Moderato__39AD958238C25821");

            entity.ToTable("Moderator");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.NikName).HasMaxLength(50);

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Moderators)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Moderator__IdCat__3D5E1FD2");
        });

        modelBuilder.Entity<Recipe>(entity =>
        {
            entity.HasKey(e => e.IdRecipe).HasName("PK__Recipe__2FEC16D4E020728D");

            entity.ToTable("Recipe");

            entity.Property(e => e.ImageRecipe).HasColumnType("image");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdCategory)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Recipe__IdCatego__4316F928");

            entity.HasOne(d => d.IdMealNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdMeal)
                .HasConstraintName("FK__Recipe__IdMeal__440B1D61");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Recipes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Recipe__IdUser__4222D4EF");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("PK__Users__B7C92638FEA13D19");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Mail).HasMaxLength(50);
            entity.Property(e => e.NikName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
