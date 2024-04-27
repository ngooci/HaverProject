﻿// <auto-generated />
using System;
using HaverProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HaverProject.Data.HMigrations
{
    [DbContext(typeof(HaverContext))]
    [Migration("20240407030915_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.17");

            modelBuilder.Entity("HaverProject.Models.ItemMarked", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemMarkeds");
                });

            modelBuilder.Entity("HaverProject.Models.ItemProblem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemProblems");
                });

            modelBuilder.Entity("HaverProject.Models.NCR", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CARNo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CARYes")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CustomerNO")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("CustomerYes")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescriptionDefect")
                        .HasColumnType("TEXT");

                    b.Property<string>("DescriptionItemID")
                        .HasColumnType("TEXT");

                    b.Property<string>("Disposition")
                        .HasColumnType("TEXT");

                    b.Property<bool>("DrawingNo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("DrawingYes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Engineer")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("EngineerDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("FollowupNo")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("FollowupYes")
                        .HasColumnType("INTEGER");

                    b.Property<string>("InspectorName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ItemMarkedID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ItemProblemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NCRNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("NameOfEngineer")
                        .HasColumnType("TEXT");

                    b.Property<string>("NcrClosed")
                        .HasColumnType("TEXT");

                    b.Property<string>("OperatingManagerName")
                        .HasColumnType("TEXT");

                    b.Property<string>("OriginalRev")
                        .HasColumnType("TEXT");

                    b.Property<long?>("PONumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("PreliminaryDecision")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PurchaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Qualitydepartment")
                        .HasColumnType("TEXT");

                    b.Property<int?>("QuantityDefected")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("QuantityReceived")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RepresentativesName")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ReviewId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("RevisingDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SapId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SapNoId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Status")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SupplierId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SupplierOrRecInspID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UpdatedRev")
                        .HasColumnType("TEXT");

                    b.Property<int?>("UseAsIsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Video")
                        .HasColumnType("TEXT");

                    b.Property<string>("VoidReason")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("finalDate")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ncrEmail")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ncrcloseno")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ncrclosenyes")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("newNcrnno")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("reino")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("reinyes")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("reviewDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("reviewno")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("reviewyes")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("ItemMarkedID");

                    b.HasIndex("ItemProblemId");

                    b.HasIndex("ReviewId");

                    b.HasIndex("SapNoId");

                    b.HasIndex("SupplierId");

                    b.HasIndex("SupplierOrRecInspID");

                    b.HasIndex("UseAsIsId");

                    b.ToTable("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.NCRComment", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CommentText")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("NCRId")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("NCRId");

                    b.ToTable("nCRComments");
                });

            modelBuilder.Entity("HaverProject.Models.NCRPhoto", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<byte[]>("Content")
                        .HasColumnType("BLOB");

                    b.Property<string>("MimeType")
                        .HasMaxLength(255)
                        .HasColumnType("TEXT");

                    b.Property<int>("NCRID")
                        .HasColumnType("INTEGER");

                    b.HasKey("ID");

                    b.HasIndex("NCRID");

                    b.ToTable("NCRPhotos");
                });

            modelBuilder.Entity("HaverProject.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("HaverProject.Models.SapNo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SapNos");
                });

            modelBuilder.Entity("HaverProject.Models.Supplier", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("HaverProject.Models.SupplierOrRecInsp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SupplierOrRecInsps");
                });

            modelBuilder.Entity("HaverProject.Models.UseAsIs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(255)
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("UseAsIss");
                });

            modelBuilder.Entity("HaverProject.ViewModel.EmailAddress", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("id");

                    b.ToTable("emailAddresses");
                });

            modelBuilder.Entity("HaverProject.Models.NCR", b =>
                {
                    b.HasOne("HaverProject.Models.ItemMarked", "ItemMarked")
                        .WithMany("Ncrs")
                        .HasForeignKey("ItemMarkedID");

                    b.HasOne("HaverProject.Models.ItemProblem", "ItemProblem")
                        .WithMany("Ncrs")
                        .HasForeignKey("ItemProblemId");

                    b.HasOne("HaverProject.Models.Review", null)
                        .WithMany("Ncrs")
                        .HasForeignKey("ReviewId");

                    b.HasOne("HaverProject.Models.SapNo", "SapNo")
                        .WithMany("Ncrs")
                        .HasForeignKey("SapNoId");

                    b.HasOne("HaverProject.Models.Supplier", "Supplier")
                        .WithMany("Ncrs")
                        .HasForeignKey("SupplierId");

                    b.HasOne("HaverProject.Models.SupplierOrRecInsp", "SupplierOrRecInsp")
                        .WithMany("Ncrs")
                        .HasForeignKey("SupplierOrRecInspID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HaverProject.Models.UseAsIs", "UseAsIs")
                        .WithMany("Ncrs")
                        .HasForeignKey("UseAsIsId");

                    b.Navigation("ItemMarked");

                    b.Navigation("ItemProblem");

                    b.Navigation("SapNo");

                    b.Navigation("Supplier");

                    b.Navigation("SupplierOrRecInsp");

                    b.Navigation("UseAsIs");
                });

            modelBuilder.Entity("HaverProject.Models.NCRComment", b =>
                {
                    b.HasOne("HaverProject.Models.NCR", null)
                        .WithMany("Comments")
                        .HasForeignKey("NCRId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("HaverProject.Models.NCRPhoto", b =>
                {
                    b.HasOne("HaverProject.Models.NCR", "NCR")
                        .WithMany("NCRPhotos")
                        .HasForeignKey("NCRID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NCR");
                });

            modelBuilder.Entity("HaverProject.Models.ItemMarked", b =>
                {
                    b.Navigation("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.ItemProblem", b =>
                {
                    b.Navigation("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.NCR", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("NCRPhotos");
                });

            modelBuilder.Entity("HaverProject.Models.Review", b =>
                {
                    b.Navigation("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.SapNo", b =>
                {
                    b.Navigation("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.Supplier", b =>
                {
                    b.Navigation("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.SupplierOrRecInsp", b =>
                {
                    b.Navigation("Ncrs");
                });

            modelBuilder.Entity("HaverProject.Models.UseAsIs", b =>
                {
                    b.Navigation("Ncrs");
                });
#pragma warning restore 612, 618
        }
    }
}
