﻿// <auto-generated />
using System;
using LaboratorySitInSystem;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LabSitInSystem.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20250103151029_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.20");

            modelBuilder.Entity("LaboratorySitInSystem.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NotificationMessage")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("NotificationTime")
                        .HasColumnType("TEXT");

                    b.Property<int>("SitInId")
                        .HasColumnType("INTEGER");

                    b.HasKey("NotificationId");

                    b.HasIndex("SitInId");

                    b.ToTable("Notifications");
                });

            modelBuilder.Entity("LaboratorySitInSystem.SitIn", b =>
                {
                    b.Property<int>("SitInId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<bool>("ApprovedByAdmin")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Duration")
                        .HasColumnType("INTEGER");

                    b.Property<string>("StudentId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("StudentUserId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("TimeIn")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("TimeOut")
                        .HasColumnType("TEXT");

                    b.HasKey("SitInId");

                    b.HasIndex("StudentUserId");

                    b.ToTable("SitIns");
                });

            modelBuilder.Entity("LaboratorySitInSystem.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Program")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Section")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("LaboratorySitInSystem.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("UserId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            FullName = "Administrator",
                            PasswordHash = "$2a$11$cwTpQ/UKnTwGf1JalcNZ6.SqM551uIn.aeMszjdfyMteBjzcBleTm",
                            Username = "admin@ctu.edu.ph"
                        });
                });

            modelBuilder.Entity("LaboratorySitInSystem.Notification", b =>
                {
                    b.HasOne("LaboratorySitInSystem.SitIn", "SitIn")
                        .WithMany()
                        .HasForeignKey("SitInId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SitIn");
                });

            modelBuilder.Entity("LaboratorySitInSystem.SitIn", b =>
                {
                    b.HasOne("LaboratorySitInSystem.User", "Student")
                        .WithMany()
                        .HasForeignKey("StudentUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });
#pragma warning restore 612, 618
        }
    }
}
