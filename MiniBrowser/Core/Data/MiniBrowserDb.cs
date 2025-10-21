using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace MiniBrowser.Core
{
    // Database setup for MiniBrowser, used when switching from JSON to SQLite)
    public class DbUser
    {
        public int Id { get; set; }
        public string Name { get; set; } = "default";
    }

    // To keep each user's saved bookmarks in the database
    public class DbBookmark
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "default";
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
    }

    // To store browsing history per user
    public class DbHistory
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "default";
        public string Url { get; set; } = "";
        public DateTime VisitedAt { get; set; } = DateTime.UtcNow;
    }

    // To store the user’s settings like home page URL
    public class DbSetting
    {
        public int Id { get; set; }
        public string UserName { get; set; } = "default";
        public string HomeUrl { get; set; } = UrlTools.DefaultHome;
    }

    // Main EF Core database context for MiniBrowser, responsible for creating tables and managing data
    public class MiniBrowserDb : DbContext
    {
        // Table references - DbSet acts like a collection for each entity
        public DbSet<DbUser> Users => Set<DbUser>();
        public DbSet<DbBookmark> Bookmarks => Set<DbBookmark>();
        public DbSet<DbHistory> History => Set<DbHistory>();
        public DbSet<DbSetting> Settings => Set<DbSetting>();

        // Database file location (inside AppData\MiniBrowser\)
        public static string DbPath =>
            Path.Combine(FileStore.AppDir, "minibrowser.db");

        // Database configuration using SQLite
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            FileStore.EnsureDir();
            options.UseSqlite($"Data Source={DbPath}");
        }

        // Set up unique constraints and indexes for cleaner data
        protected override void OnModelCreating(ModelBuilder b)
        {
            b.Entity<DbUser>()
                .HasIndex(x => x.Name)
                .IsUnique();

            b.Entity<DbBookmark>()
                .HasIndex(x => new { x.UserName, x.Url })
                .IsUnique();

            b.Entity<DbSetting>()
                .HasIndex(x => x.UserName)
                .IsUnique();
        }

        // This is called only once to creates DB if missing
        public static void EnsureSchema()
        {
            using var db = new MiniBrowserDb();
            // To Create DB and tables automatically
            db.Database.EnsureCreated();
        }
    }
}
