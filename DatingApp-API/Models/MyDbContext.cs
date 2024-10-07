using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace App_Dating.Models;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Match> Matches { get; set; }

    public virtual DbSet<Preference> Preferences { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPreference> UserPreferences { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Match>(entity =>
        {
            entity.ToTable("Match");

            entity.HasOne(d => d.IdUserSourceNavigation).WithMany(p => p.MatchIdUserSourceNavigations)
                .HasForeignKey(d => d.IdUserSource)
                .HasConstraintName("FK_Match_User_Source");

            entity.HasOne(d => d.IdUserTargetNavigation).WithMany(p => p.MatchIdUserTargetNavigations)
                .HasForeignKey(d => d.IdUserTarget)
                .HasConstraintName("FK_Match_User_Target");
        });

        modelBuilder.Entity<Preference>(entity =>
        {
            entity.ToTable("Preference");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Email).HasMaxLength(200);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Gender)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.LastName).HasMaxLength(150);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserPreference>(entity =>
        {
            entity.ToTable("UserPreference");

            entity.HasOne(d => d.IdPreferencesNavigation).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.IdPreferences)
                .HasConstraintName("FK_UserPreference_Preference");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UserPreferences)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK_UserPreference_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
