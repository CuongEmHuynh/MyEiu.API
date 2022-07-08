﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyEiu.Data.EF.DbContexts;

#nullable disable

namespace MyEiu.Data.EF.Migrations
{
    [DbContext(typeof(MobileAppDbContext))]
    [Migration("20220705024549_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MyEiu.Data.Entities.App.FileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("DisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FileData");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("CreateBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Disable")
                        .HasColumnType("bit");

                    b.Property<int?>("ModifyBy")
                        .HasColumnType("int");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("PostTypeId")
                        .HasColumnType("int");

                    b.Property<int?>("Priority")
                        .HasColumnType("int");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CreateBy");

                    b.HasIndex("ModifyBy");

                    b.HasIndex("PostTypeId");

                    b.ToTable("Post");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Content = "Sample",
                            CreateBy = 1,
                            CreateDate = new DateTime(2022, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Description = "Sample",
                            Disable = false,
                            PostTypeId = 1,
                            Priority = 0,
                            Status = 0,
                            Title = "Sample"
                        });
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostFileData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("FileDataId")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileDataId")
                        .IsUnique()
                        .HasFilter("[FileDataId] IS NOT NULL");

                    b.HasIndex("PostId");

                    b.ToTable("PostFileData");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostGroup");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PostType");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Gửi thông báo sự kiện đến người dùng",
                            Name = "Thông báo"
                        });
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("GroupId")
                        .HasColumnType("int");

                    b.Property<int?>("PostId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.ToTable("PostUser");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.UserApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Code")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("IsDeleted")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("UserApp");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthday = new DateTime(1988, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "040016",
                            Email = "ngu.nguyen@eiu.edu.vn",
                            FirstName = "Ngữ",
                            IsDeleted = 0,
                            LastName = "Nguyễn",
                            Phone = "0977317173",
                            RoleId = 2,
                            Username = "ngu.nguyen"
                        },
                        new
                        {
                            Id = 2,
                            Birthday = new DateTime(1997, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Code = "040017",
                            Email = "em.huynh@eiu.edu.vn",
                            FirstName = "Em",
                            IsDeleted = 0,
                            LastName = "Huynh",
                            Phone = "0977888888",
                            RoleId = 2,
                            Username = "em.huynh"
                        });
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("UserRole");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Người quản trị hệ thống",
                            Name = "Admin"
                        },
                        new
                        {
                            Id = 2,
                            Description = "Người dùng phần mềm",
                            Name = "User"
                        });
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.Post", b =>
                {
                    b.HasOne("MyEiu.Data.Entities.App.UserApp", "Author")
                        .WithMany("PostAuthors")
                        .HasForeignKey("CreateBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyEiu.Data.Entities.App.UserApp", "Editor")
                        .WithMany("PostEditors")
                        .HasForeignKey("ModifyBy")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("MyEiu.Data.Entities.App.PostType", "PostType")
                        .WithMany("Posts")
                        .HasForeignKey("PostTypeId");

                    b.Navigation("Author");

                    b.Navigation("Editor");

                    b.Navigation("PostType");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostFileData", b =>
                {
                    b.HasOne("MyEiu.Data.Entities.App.FileData", "FileData")
                        .WithOne("PostFileData")
                        .HasForeignKey("MyEiu.Data.Entities.App.PostFileData", "FileDataId");

                    b.HasOne("MyEiu.Data.Entities.App.Post", "Post")
                        .WithMany("PostFileDatas")
                        .HasForeignKey("PostId");

                    b.Navigation("FileData");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostGroup", b =>
                {
                    b.HasOne("MyEiu.Data.Entities.App.Post", "Post")
                        .WithMany("PostGroups")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostUser", b =>
                {
                    b.HasOne("MyEiu.Data.Entities.App.Post", "Post")
                        .WithMany("PostUsers")
                        .HasForeignKey("PostId");

                    b.Navigation("Post");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.UserApp", b =>
                {
                    b.HasOne("MyEiu.Data.Entities.App.UserRole", "UserRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("UserRole");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.FileData", b =>
                {
                    b.Navigation("PostFileData");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.Post", b =>
                {
                    b.Navigation("PostFileDatas");

                    b.Navigation("PostGroups");

                    b.Navigation("PostUsers");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.PostType", b =>
                {
                    b.Navigation("Posts");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.UserApp", b =>
                {
                    b.Navigation("PostAuthors");

                    b.Navigation("PostEditors");
                });

            modelBuilder.Entity("MyEiu.Data.Entities.App.UserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
