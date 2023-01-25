﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using UserListsMVC.DataLayer;

#nullable disable

namespace UserListsMVC.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230125152611_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("text");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("text");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.CustomListItem", b =>
                {
                    b.Property<int>("ListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ListItemId"));

                    b.Property<DateOnly>("DateAdded")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FinishedDate")
                        .HasColumnType("date");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemPoster")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ItemStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ItemTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonalScore")
                        .HasColumnType("integer");

                    b.Property<int>("PersonalVote")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("PlannedDate")
                        .HasColumnType("date");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("StartedDate")
                        .HasColumnType("date");

                    b.Property<int>("UserListId")
                        .HasColumnType("integer");

                    b.HasKey("ListItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserListId");

                    b.ToTable("CustomListItem");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.FollowlistItem", b =>
                {
                    b.Property<int>("ListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ListItemId"));

                    b.Property<DateOnly>("DateAdded")
                        .HasColumnType("date");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemPoster")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Notifications")
                        .HasColumnType("boolean");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<int>("UserListId")
                        .HasColumnType("integer");

                    b.HasKey("ListItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserListId");

                    b.ToTable("FollowlistItem");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.CustomListItem>", b =>
                {
                    b.Property<int>("UserListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserListId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<int>("ListType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserListId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserList<CustomListItem>");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.FollowlistItem>", b =>
                {
                    b.Property<int>("UserListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserListId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<int>("ListType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserListId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserList<FollowlistItem>");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.WishlistItem>", b =>
                {
                    b.Property<int>("UserListId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("UserListId"));

                    b.Property<string>("ApplicationUserId")
                        .HasColumnType("text");

                    b.Property<int>("ContentType")
                        .HasColumnType("integer");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean");

                    b.Property<int>("ListType")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserListId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("UserList<WishlistItem>");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.WishlistItem", b =>
                {
                    b.Property<int>("ListItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("ListItemId"));

                    b.Property<DateOnly>("DateAdded")
                        .HasColumnType("date");

                    b.Property<DateOnly>("FinishedDate")
                        .HasColumnType("date");

                    b.Property<string>("ItemId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ItemPoster")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("ItemStatus")
                        .HasColumnType("integer");

                    b.Property<string>("ItemTitle")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("PersonalScore")
                        .HasColumnType("integer");

                    b.Property<int>("PersonalVote")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("PlannedDate")
                        .HasColumnType("date");

                    b.Property<int>("Position")
                        .HasColumnType("integer");

                    b.Property<DateOnly>("StartedDate")
                        .HasColumnType("date");

                    b.Property<int>("UserListId")
                        .HasColumnType("integer");

                    b.HasKey("ListItemId");

                    b.HasIndex("ItemId");

                    b.HasIndex("UserListId");

                    b.ToTable("WishlistItem");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.CustomListItem", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.CustomListItem>", "UserList")
                        .WithMany("UserListItems")
                        .HasForeignKey("UserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserList");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.FollowlistItem", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.FollowlistItem>", "UserList")
                        .WithMany("UserListItems")
                        .HasForeignKey("UserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserList");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.CustomListItem>", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany("UserCustomLists")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.FollowlistItem>", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany("UserFollowlists")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.WishlistItem>", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.ApplicationUser", null)
                        .WithMany("UserWishlists")
                        .HasForeignKey("ApplicationUserId");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.WishlistItem", b =>
                {
                    b.HasOne("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.WishlistItem>", "UserList")
                        .WithMany("UserListItems")
                        .HasForeignKey("UserListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserList");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.ApplicationUser", b =>
                {
                    b.Navigation("UserCustomLists");

                    b.Navigation("UserFollowlists");

                    b.Navigation("UserWishlists");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.CustomListItem>", b =>
                {
                    b.Navigation("UserListItems");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.FollowlistItem>", b =>
                {
                    b.Navigation("UserListItems");
                });

            modelBuilder.Entity("UserListsMVC.DataLayer.Entities.UserList<UserListsMVC.DataLayer.Entities.WishlistItem>", b =>
                {
                    b.Navigation("UserListItems");
                });
#pragma warning restore 612, 618
        }
    }
}
