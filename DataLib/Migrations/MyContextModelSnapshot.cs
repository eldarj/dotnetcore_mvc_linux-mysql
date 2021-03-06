﻿// <auto-generated />
using System;
using DataLib;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DataLib.Migrations
{
    [DbContext(typeof(MyContext))]
    partial class MyContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("DataLib.Models.AuthToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateGenerated");

                    b.Property<string>("Ip");

                    b.Property<int?>("ModeratorId");

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.HasIndex("ModeratorId");

                    b.ToTable("AuthTokens");
                });

            modelBuilder.Entity("DataLib.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Name");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataLib.Models.Moderator", b =>
                {
                    b.Property<int>("UserID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateRegistered");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UserID");

                    b.ToTable("Moderators");
                });

            modelBuilder.Entity("DataLib.Models.Post", b =>
                {
                    b.Property<int>("PostID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<int?>("CategoryId");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Description");

                    b.Property<int>("Hits");

                    b.Property<string>("IntroImageUrl");

                    b.Property<int>("Likes");

                    b.Property<int?>("ModeratorId");

                    b.Property<int>("SharesFacebook");

                    b.Property<int>("SharesTwitter");

                    b.Property<string>("Title");

                    b.HasKey("PostID");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ModeratorId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("DataLib.Models.AuthToken", b =>
                {
                    b.HasOne("DataLib.Models.Moderator", "Moderator")
                        .WithMany()
                        .HasForeignKey("ModeratorId");
                });

            modelBuilder.Entity("DataLib.Models.Post", b =>
                {
                    b.HasOne("DataLib.Models.Category", "Category")
                        .WithMany("Posts")
                        .HasForeignKey("CategoryId");

                    b.HasOne("DataLib.Models.Moderator", "Moderator")
                        .WithMany("Posts")
                        .HasForeignKey("ModeratorId");
                });
#pragma warning restore 612, 618
        }
    }
}
