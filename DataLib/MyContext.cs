﻿using System;
using DataLib.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore;

namespace DataLib
{
    public class MyContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(c => c.CategoryID);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(p => p.PostID);
                entity.HasOne(p => p.Category).WithMany(c => c.Posts);
                entity.HasOne(p => p.Moderator).WithMany(a => a.Posts);
            });

            modelBuilder.Entity<Moderator>(entity =>
            {
                entity.HasKey(a => a.UserID);
            });
        }
    }
}
