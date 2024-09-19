﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SPTKnowledgeService.Data;

#nullable disable

namespace SPTKnowledgeService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("SPTKnowledgeService.Models.Break", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("StudySessionId")
                        .HasColumnType("int");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("StudySessionId")
                        .IsUnique();

                    b.ToTable("Breaks");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.BreakDuration", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BreakId")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("Duration")
                        .HasColumnType("time");

                    b.HasKey("Id");

                    b.HasIndex("BreakId");

                    b.ToTable("BreakDuration");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.Grade", b =>
                {
                    b.Property<string>("code")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("code");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.Knowledge", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("GradeCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Knowledge");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.StudySession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<string>("SubjectCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StudySession");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.Break", b =>
                {
                    b.HasOne("SPTKnowledgeService.Models.StudySession", "StudySession")
                        .WithOne("Break")
                        .HasForeignKey("SPTKnowledgeService.Models.Break", "StudySessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudySession");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.BreakDuration", b =>
                {
                    b.HasOne("SPTKnowledgeService.Models.Break", "Break")
                        .WithMany("BreakDurations")
                        .HasForeignKey("BreakId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Break");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.Break", b =>
                {
                    b.Navigation("BreakDurations");
                });

            modelBuilder.Entity("SPTKnowledgeService.Models.StudySession", b =>
                {
                    b.Navigation("Break")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
