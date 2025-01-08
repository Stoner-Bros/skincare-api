﻿// <auto-generated />
using System;
using APP.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APP.API.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250108071855_InitAppDb")]
    partial class InitAppDb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APP.Entity.Entities.Account", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountId"));

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("avatar");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("created_date");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("full_name");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("is_active");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit")
                        .HasColumnName("is_deleted");

                    b.Property<DateTime?>("LastLogin")
                        .HasColumnType("datetime2")
                        .HasColumnName("last_login");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("password");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("role");

                    b.HasKey("AccountId");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("account", "dbo", t =>
                        {
                            t.HasCheckConstraint("CK_Email_Length", "LEN([email]) >= 6");

                            t.HasCheckConstraint("CK_Email_Valid", "CHARINDEX('@', [email]) > 0");

                            t.HasCheckConstraint("CK_Fullname_Length", "LEN([full_name]) >= 6");

                            t.HasCheckConstraint("CK_Role_Valid", "[role] IN ('Customer', 'Skin Therapist', 'Staff', 'Manager')");
                        });
                });

            modelBuilder.Entity("APP.Entity.Entities.AccountInfo", b =>
                {
                    b.Property<int>("AccountInfoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("account_info_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AccountInfoId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("address");

                    b.Property<string>("Background")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("background");

                    b.Property<string>("Bio")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("bio");

                    b.Property<DateOnly?>("Dob")
                        .HasColumnType("date")
                        .HasColumnName("dob");

                    b.Property<string>("OtherInfo")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("other_info");

                    b.Property<string>("Phone")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("phone");

                    b.HasKey("AccountInfoId");

                    b.HasIndex("AccountId")
                        .IsUnique();

                    b.ToTable("account_info", "dbo", t =>
                        {
                            t.HasCheckConstraint("CK_Phone_Valid", "LEN([phone]) = 10");
                        });
                });

            modelBuilder.Entity("APP.Entity.Entities.ExpiredToken", b =>
                {
                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("token");

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("InvalidationTime")
                        .HasColumnType("datetime2")
                        .HasColumnName("invalidation_time");

                    b.HasKey("Token");

                    b.HasIndex("AccountId");

                    b.ToTable("expired_token", "dbo");
                });

            modelBuilder.Entity("APP.Entity.Entities.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("refresh_token_id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RefreshTokenId"));

                    b.Property<int>("AccountId")
                        .HasColumnType("int")
                        .HasColumnName("account_id");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2")
                        .HasColumnName("created");

                    b.Property<DateTime>("Expiry")
                        .HasColumnType("datetime2")
                        .HasColumnName("expiry");

                    b.Property<DateTime?>("Revoked")
                        .HasColumnType("datetime2")
                        .HasColumnName("revoked");

                    b.Property<string>("Token")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("token");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("AccountId");

                    b.ToTable("refresh_token", "dbo");
                });

            modelBuilder.Entity("APP.Entity.Entities.AccountInfo", b =>
                {
                    b.HasOne("APP.Entity.Entities.Account", "Account")
                        .WithOne("AccountInfo")
                        .HasForeignKey("APP.Entity.Entities.AccountInfo", "AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("APP.Entity.Entities.ExpiredToken", b =>
                {
                    b.HasOne("APP.Entity.Entities.Account", "Account")
                        .WithMany("ExpiredTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("APP.Entity.Entities.RefreshToken", b =>
                {
                    b.HasOne("APP.Entity.Entities.Account", "Account")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("APP.Entity.Entities.Account", b =>
                {
                    b.Navigation("AccountInfo")
                        .IsRequired();

                    b.Navigation("ExpiredTokens");

                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}
