﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MoneyMeAPI.Data;

#nullable disable

namespace MoneyMeAPI.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20221110040958_AddBlackList")]
    partial class AddBlackList
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.10");

            modelBuilder.Entity("MoneyMeAPI.Model.AccountModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Mobile")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FirstName", "LastName", "DateOfBirth")
                        .IsUnique();

                    b.ToTable("Accounts");
                });

            modelBuilder.Entity("MoneyMeAPI.Model.BlacklistModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("type")
                        .HasColumnType("TEXT");

                    b.Property<string>("value")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Blacklists");
                });

            modelBuilder.Entity("MoneyMeAPI.Model.LoanModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Product")
                        .HasColumnType("TEXT");

                    b.Property<int>("Term")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("MoneyMeAPI.Model.RepaymentsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("AccountId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Amount")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("EstablishmentFee")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Interest")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("Repayments");
                });

            modelBuilder.Entity("MoneyMeAPI.Model.LoanModel", b =>
                {
                    b.HasOne("MoneyMeAPI.Model.AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("MoneyMeAPI.Model.RepaymentsModel", b =>
                {
                    b.HasOne("MoneyMeAPI.Model.AccountModel", "Account")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });
#pragma warning restore 612, 618
        }
    }
}
