﻿// <auto-generated />
using System;
using DataAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAPI.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20240310043649_SecondMigration")]
    partial class SecondMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("DataAPI.Models.Business", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("BusinessName")
                        .HasColumnType("TEXT");

                    b.Property<string>("BusinessOwner")
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsActive")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Businesses");
                });

            modelBuilder.Entity("DataAPI.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<bool>("AllowCredit")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.ToTable("Customer");
                });

            modelBuilder.Entity("DataAPI.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CustomerName")
                        .HasColumnType("TEXT");

                    b.Property<double>("Total")
                        .HasColumnType("REAL");

                    b.HasKey("ID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("DataAPI.Models.OrderProduct", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("OrderID");

                    b.HasIndex("ProductID");

                    b.ToTable("OrderProducts");
                });

            modelBuilder.Entity("DataAPI.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("BusinessID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("DataAPI.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BusinessId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsVerified")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BusinessId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("IdentityUsers");
                });

            modelBuilder.Entity("DataAPI.Models.Customer", b =>
                {
                    b.HasOne("DataAPI.Models.Business", "Business")
                        .WithMany("Customers")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("DataAPI.Models.OrderProduct", b =>
                {
                    b.HasOne("DataAPI.Models.Order", "Order")
                        .WithMany()
                        .HasForeignKey("OrderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAPI.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DataAPI.Models.Product", b =>
                {
                    b.HasOne("DataAPI.Models.Business", "Business")
                        .WithMany("Products")
                        .HasForeignKey("BusinessID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Business");
                });

            modelBuilder.Entity("DataAPI.Models.User", b =>
                {
                    b.HasOne("DataAPI.Models.Business", "UserBusiness")
                        .WithMany("Users")
                        .HasForeignKey("BusinessId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserBusiness");
                });

            modelBuilder.Entity("DataAPI.Models.Business", b =>
                {
                    b.Navigation("Customers");

                    b.Navigation("Products");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}