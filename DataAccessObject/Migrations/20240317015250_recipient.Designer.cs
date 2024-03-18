﻿// <auto-generated />
using System;
using DataAccessObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DataAccessObject.Migrations
{
    [DbContext(typeof(SqlDbContext))]
    [Migration("20240317015250_recipient")]
    partial class recipient
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BusinessObject.SqlObject.ArtInfo", b =>
                {
                    b.Property<int>("ArtId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ArtId"));

                    b.Property<string>("ArtName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ArtId");

                    b.HasIndex("CreatorId");

                    b.ToTable("ArtInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.ArtRating", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("ArtId")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime>("RatingDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "ArtId");

                    b.HasIndex("ArtId");

                    b.ToTable("ArtRating");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.ArtTag", b =>
                {
                    b.Property<int>("ArtId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("ArtId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ArtTag");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Commission", b =>
                {
                    b.Property<int>("CommissionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CommissionId"));

                    b.Property<int>("CommissionStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ImageId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int?>("Rating")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RatingDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Review")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("CommissionId");

                    b.HasIndex("CreatorId");

                    b.HasIndex("UserId");

                    b.ToTable("Commission");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.CreatorInfo", b =>
                {
                    b.Property<int>("CreatorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CreatorId"));

                    b.Property<int>("AcceptCommissionStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("BecomeArtistDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Bio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContactInfo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CreatorId");

                    b.ToTable("CreatorInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Follow", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FollowDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "CreatorId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Follow");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PostId"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("CreatorId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Visibility")
                        .HasColumnType("int");

                    b.HasKey("PostId");

                    b.HasIndex("CreatorId");

                    b.ToTable("Post");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.PostLike", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<int>("PostId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LikedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("UserId", "PostId");

                    b.HasIndex("PostId");

                    b.ToTable("PostLike");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Purchase", b =>
                {
                    b.Property<int>("PurchaseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PurchaseId"));

                    b.Property<int>("ArtId")
                        .HasColumnType("int");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("PurchaseId");

                    b.HasIndex("ArtId");

                    b.HasIndex("UserId");

                    b.ToTable("Purchase");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Report", b =>
                {
                    b.Property<int>("ReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReportId"));

                    b.Property<DateTime>("ReportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReportDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportReason")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ReportedObjectId")
                        .HasColumnType("int");

                    b.Property<int>("ReportedObjectType")
                        .HasColumnType("int");

                    b.Property<int>("ReporterId")
                        .HasColumnType("int");

                    b.HasKey("ReportId");

                    b.HasIndex("ReporterId");

                    b.ToTable("Report");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Tag", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TagId"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("TagId");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.TransactionHistory", b =>
                {
                    b.Property<int>("TransactionId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TransactionId"));

                    b.Property<double>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("Note")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransactionType")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TransactionId");

                    b.HasIndex("UserId");

                    b.ToTable("TransactionHistorie");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<double>("Balance")
                        .HasColumnType("float");

                    b.Property<int?>("CreatorId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("JoinDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NickName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("CreatorId")
                        .IsUnique()
                        .HasFilter("[CreatorId] IS NOT NULL");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.ArtInfo", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.CreatorInfo", "CreatorInfo")
                        .WithMany("ArtInfos")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.ArtRating", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.ArtInfo", "ArtInfo")
                        .WithMany("ArtRatings")
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.SqlObject.UserInfo", "UserInfo")
                        .WithMany("ArtRatings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtInfo");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.ArtTag", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.ArtInfo", null)
                        .WithMany("ArtTags")
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.SqlObject.Tag", "Tag")
                        .WithMany()
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Commission", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.CreatorInfo", "CreatorInfo")
                        .WithMany("Commissions")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.SqlObject.UserInfo", "UserInfo")
                        .WithMany("Commissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorInfo");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Follow", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.CreatorInfo", "CreatorInfo")
                        .WithMany("Follows")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.SqlObject.UserInfo", "UserInfo")
                        .WithMany("Follows")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorInfo");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Post", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.CreatorInfo", "CreatorInfo")
                        .WithMany("Posts")
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatorInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.PostLike", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.Post", "Post")
                        .WithMany("PostLikes")
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.SqlObject.UserInfo", "UserInfo")
                        .WithMany("PostLikes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Post");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Purchase", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.ArtInfo", "ArtInfo")
                        .WithMany("Purchases")
                        .HasForeignKey("ArtId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObject.SqlObject.UserInfo", "UserInfo")
                        .WithMany("Purchases")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ArtInfo");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Report", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.UserInfo", "Reporter")
                        .WithMany("Reports")
                        .HasForeignKey("ReporterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reporter");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.TransactionHistory", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.UserInfo", "UserInfo")
                        .WithMany("TransactionHistory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.UserInfo", b =>
                {
                    b.HasOne("BusinessObject.SqlObject.CreatorInfo", "CreatorInfo")
                        .WithOne("UserInfo")
                        .HasForeignKey("BusinessObject.SqlObject.UserInfo", "CreatorId");

                    b.Navigation("CreatorInfo");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.ArtInfo", b =>
                {
                    b.Navigation("ArtRatings");

                    b.Navigation("ArtTags");

                    b.Navigation("Purchases");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.CreatorInfo", b =>
                {
                    b.Navigation("ArtInfos");

                    b.Navigation("Commissions");

                    b.Navigation("Follows");

                    b.Navigation("Posts");

                    b.Navigation("UserInfo")
                        .IsRequired();
                });

            modelBuilder.Entity("BusinessObject.SqlObject.Post", b =>
                {
                    b.Navigation("PostLikes");
                });

            modelBuilder.Entity("BusinessObject.SqlObject.UserInfo", b =>
                {
                    b.Navigation("ArtRatings");

                    b.Navigation("Commissions");

                    b.Navigation("Follows");

                    b.Navigation("PostLikes");

                    b.Navigation("Purchases");

                    b.Navigation("Reports");

                    b.Navigation("TransactionHistory");
                });
#pragma warning restore 612, 618
        }
    }
}
