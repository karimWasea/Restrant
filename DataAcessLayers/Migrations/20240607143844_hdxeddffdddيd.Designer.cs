﻿// <auto-generated />
using System;
using DataAcessLayers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAcessLayers.Migrations
{
    [DbContext(typeof(ApplicationDBcontext))]
    [Migration("20240607143844_hdxeddffdddيd")]
    partial class hdxeddffdddيd
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DataAcessLayers.Applicaionuser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CustomerType")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("DataAcessLayers.Category", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("CategoryName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("DataAcessLayers.CategoryAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId1")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId1");

                    b.ToTable("CategoryAttachments");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialAdvanceHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChangedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NotPayedmoneyId")
                        .HasColumnType("int");

                    b.Property<decimal?>("NotpayedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("PayedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int>("PriceProductebytypesid")
                        .HasColumnType("int");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserNotPayedmoneyId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("NotPayedmoneyId");

                    b.HasIndex("PriceProductebytypesid");

                    b.HasIndex("UserNotPayedmoneyId");

                    b.ToTable("FinancialAdvanceHistories");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCash", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<decimal?>("PayedTotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FinancialUserCash");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCashHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("FinancialUserCashId")
                        .HasColumnType("int");

                    b.Property<decimal?>("PayedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int?>("Qantity")
                        .HasColumnType("int");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FinancialUserCashId");

                    b.ToTable("FinancialUserCashHistories");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCashHistoryPriceProductebytypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("PriceProductebytypesid")
                        .HasColumnType("int");

                    b.Property<int>("financialUserCashHistoryid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PriceProductebytypesid");

                    b.HasIndex("financialUserCashHistoryid");

                    b.ToTable("FinancialUserCashHistoryPriceProductebytypes");
                });

            modelBuilder.Entity("DataAcessLayers.NotPayedmoney", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ApplicaionuserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("ChangeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ChangedByUserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("PaymentStatus")
                        .HasColumnType("int");

                    b.Property<int?>("PriceProductebytypesId")
                        .HasColumnType("int");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("TotalNotpayedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("TotalPayedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("ApplicaionuserId");

                    b.HasIndex("PriceProductebytypesId");

                    b.ToTable("NotPayedmoney");
                });

            modelBuilder.Entity("DataAcessLayers.PriceProductebytypes", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("CustomerType")
                        .HasColumnType("int");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int?>("Qantity")
                        .HasColumnType("int");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("price")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("PriceProductebytypes");
                });

            modelBuilder.Entity("DataAcessLayers.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryTyPe")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Discount")
                        .HasColumnType("int");

                    b.Property<decimal?>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ProductName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal?>("Qantity")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("DataAcessLayers.ProductAttachment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreationTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FileType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<string>("SystemUserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SystemUserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductAttachments");
                });

            modelBuilder.Entity("DataAcessLayers.ShopingCaterCashHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("PayedAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("PriceProductebytypesId")
                        .HasColumnType("int");

                    b.Property<int?>("Qantity")
                        .HasColumnType("int");

                    b.Property<int>("catid")
                        .HasColumnType("int");

                    b.Property<string>("productName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("productid")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("ShopingCaterCashHistory");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("DataAcessLayers.CategoryAttachment", b =>
                {
                    b.HasOne("DataAcessLayers.Category", "Category")
                        .WithMany("CategoryAttachment")
                        .HasForeignKey("CategoryId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialAdvanceHistory", b =>
                {
                    b.HasOne("DataAcessLayers.NotPayedmoney", "NotPayedmoneys")
                        .WithMany("FinancialAdvanceHistory")
                        .HasForeignKey("NotPayedmoneyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAcessLayers.PriceProductebytypes", "PriceProductebytypes")
                        .WithMany()
                        .HasForeignKey("PriceProductebytypesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAcessLayers.Applicaionuser", "UserNotPayedmoney")
                        .WithMany()
                        .HasForeignKey("UserNotPayedmoneyId");

                    b.Navigation("NotPayedmoneys");

                    b.Navigation("PriceProductebytypes");

                    b.Navigation("UserNotPayedmoney");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCashHistory", b =>
                {
                    b.HasOne("DataAcessLayers.FinancialUserCash", "FinancialUserCash")
                        .WithMany("FinancialUserCashHistory")
                        .HasForeignKey("FinancialUserCashId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("FinancialUserCash");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCashHistoryPriceProductebytypes", b =>
                {
                    b.HasOne("DataAcessLayers.PriceProductebytypes", "PriceProductebytypes")
                        .WithMany("FinancialUserCashHistoryPriceProductebytypes")
                        .HasForeignKey("PriceProductebytypesid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DataAcessLayers.FinancialUserCashHistory", "financialUserCashHistory")
                        .WithMany("FinancialUserCashHistoryPriceProductebytypes")
                        .HasForeignKey("financialUserCashHistoryid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PriceProductebytypes");

                    b.Navigation("financialUserCashHistory");
                });

            modelBuilder.Entity("DataAcessLayers.NotPayedmoney", b =>
                {
                    b.HasOne("DataAcessLayers.Applicaionuser", null)
                        .WithMany("NotPayedmoney")
                        .HasForeignKey("ApplicaionuserId");

                    b.HasOne("DataAcessLayers.PriceProductebytypes", null)
                        .WithMany("NotPayedmoney")
                        .HasForeignKey("PriceProductebytypesId");
                });

            modelBuilder.Entity("DataAcessLayers.PriceProductebytypes", b =>
                {
                    b.HasOne("DataAcessLayers.Product", "Product")
                        .WithMany("PriceProductebytypes")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("DataAcessLayers.Product", b =>
                {
                    b.HasOne("DataAcessLayers.Category", null)
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");
                });

            modelBuilder.Entity("DataAcessLayers.ProductAttachment", b =>
                {
                    b.HasOne("DataAcessLayers.Product", "Product")
                        .WithMany("ProductAttachment")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
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
                    b.HasOne("DataAcessLayers.Applicaionuser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("DataAcessLayers.Applicaionuser", null)
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

                    b.HasOne("DataAcessLayers.Applicaionuser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("DataAcessLayers.Applicaionuser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DataAcessLayers.Applicaionuser", b =>
                {
                    b.Navigation("NotPayedmoney");
                });

            modelBuilder.Entity("DataAcessLayers.Category", b =>
                {
                    b.Navigation("CategoryAttachment");

                    b.Navigation("Products");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCash", b =>
                {
                    b.Navigation("FinancialUserCashHistory");
                });

            modelBuilder.Entity("DataAcessLayers.FinancialUserCashHistory", b =>
                {
                    b.Navigation("FinancialUserCashHistoryPriceProductebytypes");
                });

            modelBuilder.Entity("DataAcessLayers.NotPayedmoney", b =>
                {
                    b.Navigation("FinancialAdvanceHistory");
                });

            modelBuilder.Entity("DataAcessLayers.PriceProductebytypes", b =>
                {
                    b.Navigation("FinancialUserCashHistoryPriceProductebytypes");

                    b.Navigation("NotPayedmoney");
                });

            modelBuilder.Entity("DataAcessLayers.Product", b =>
                {
                    b.Navigation("PriceProductebytypes");

                    b.Navigation("ProductAttachment");
                });
#pragma warning restore 612, 618
        }
    }
}
