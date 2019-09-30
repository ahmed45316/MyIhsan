using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using MyIhsan.Identity.Entities.Entities;

namespace MyIhsan.Identity.Data.Context
{
    public partial class ModelContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public ModelContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
       
        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsersRoles> AspNetUsersRoles { get; set; }
        public virtual DbSet<MenuRoles> MenuRoles { get; set; }
        public virtual DbSet<Menus> Menus { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:DefaultSchema", "IDENTITYDB");

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("AspNetRoles_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NVARCHAR2(256)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnType("NVARCHAR2(256)");
            });

            modelBuilder.Entity<AspNetUsersRoles>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("AspNetUsersRoles_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NVARCHAR2(256)")
                    .ValueGeneratedNever();

                entity.Property(e => e.RoleId).HasColumnType("NVARCHAR2(256)");

                entity.Property(e => e.UserId).HasColumnType("NVARCHAR2(256)");
            });

            modelBuilder.Entity<MenuRoles>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("MenuRoles_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NVARCHAR2(256)")
                    .ValueGeneratedNever();

                entity.Property(e => e.MenuId).HasColumnType("NVARCHAR2(256)");

                entity.Property(e => e.RoleId).HasColumnType("NVARCHAR2(256)");
            });

            modelBuilder.Entity<Menus>(entity =>
            {
                entity.HasIndex(e => e.Id)
                    .HasName("Menus_PK")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnType("NVARCHAR2(256)")
                    .ValueGeneratedNever();

                entity.Property(e => e.Action).HasColumnType("NVARCHAR2(100)");

                entity.Property(e => e.Controller).HasColumnType("NVARCHAR2(100)");

                entity.Property(e => e.Href).HasColumnType("NVARCHAR2(256)");

                entity.Property(e => e.Icon).HasColumnType("NVARCHAR2(100)");

                entity.Property(e => e.Parameters).HasColumnType("NVARCHAR2(50)");

                entity.Property(e => e.ParentId).HasColumnType("NVARCHAR2(256)");

                entity.Property(e => e.ScreenNameAr).HasColumnType("NVARCHAR2(256)");

                entity.Property(e => e.ScreenNameEn).HasColumnType("NVARCHAR2(256)");
            });
        }
    }
}
