﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using app.Repositories;

#nullable disable

namespace app.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("app.Models.Hero", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Class")
                        .HasColumnType("int");

                    b.Property<int>("MaxHP")
                        .HasColumnType("int");

                    b.Property<int>("MaxMP")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Heroes");
                });

            modelBuilder.Entity("app.Models.HeroInstance", b =>
                {
                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("HeroId")
                        .HasColumnType("int");

                    b.Property<int>("CurrentHP")
                        .HasColumnType("int");

                    b.Property<int>("CurrentMP")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.HasKey("OwnerId", "HeroId");

                    b.HasIndex("HeroId");

                    b.ToTable("HeroInstances");
                });

            modelBuilder.Entity("app.Models.Inventory", b =>
                {
                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("OwnerId", "ItemId");

                    b.HasIndex("ItemId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("app.Models.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("app.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ActionPoints")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Money")
                        .HasColumnType("int");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varbinary(64)");

                    b.Property<int>("Score")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("app.Models.HeroInstance", b =>
                {
                    b.HasOne("app.Models.Hero", "Hero")
                        .WithMany("Instances")
                        .HasForeignKey("HeroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Models.User", "Owner")
                        .WithMany("OwnedHeroes")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hero");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("app.Models.Inventory", b =>
                {
                    b.HasOne("app.Models.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Models.User", "Owner")
                        .WithMany("OwnedItems")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("app.Models.Item", b =>
                {
                    b.HasOne("app.Models.Item", null)
                        .WithMany("OwnedItems")
                        .HasForeignKey("ItemId");
                });

            modelBuilder.Entity("app.Models.Hero", b =>
                {
                    b.Navigation("Instances");
                });

            modelBuilder.Entity("app.Models.Item", b =>
                {
                    b.Navigation("OwnedItems");
                });

            modelBuilder.Entity("app.Models.User", b =>
                {
                    b.Navigation("OwnedHeroes");

                    b.Navigation("OwnedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
