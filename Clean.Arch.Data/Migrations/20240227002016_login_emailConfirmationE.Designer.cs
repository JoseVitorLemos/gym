﻿// <auto-generated />
using System;
using Clean.Arch.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Clean.Arch.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240227002016_login_emailConfirmationE")]
    partial class login_emailConfirmationE
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ImageExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberSeries")
                        .HasPrecision(2)
                        .HasColumnType("int");

                    b.Property<int>("Repetitions")
                        .HasPrecision(2)
                        .HasColumnType("int");

                    b.Property<int?>("RestTime")
                        .IsRequired()
                        .HasPrecision(2)
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.Property<Guid>("WorkoutId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ImageExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("EXERCISES", (string)null);
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.ImageExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ExerciseName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<byte[]>("FileByte")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ExerciseName")
                        .IsUnique();

                    b.ToTable("IMAGE_EXERCISES", (string)null);
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.IndividualEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<int>("Gender")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.ToTable("INDIVIDUAL_ENTITIES", (string)null);
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Login", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmation")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Login");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Professional", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Cref")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IndividualEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IndividualEntityId");

                    b.ToTable("PROFESSIONALS", (string)null);
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IndividualEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IndividualEntityId");

                    b.HasIndex("LoginId");

                    b.ToTable("USERS", (string)null);
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Division")
                        .HasColumnType("int");

                    b.Property<Guid?>("ImageExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IndividualEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("ImageExerciseId");

                    b.HasIndex("IndividualEntityId");

                    b.HasIndex("PersonalId");

                    b.ToTable("WORKOUTS", (string)null);
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Exercise", b =>
                {
                    b.HasOne("Clean.Arch.Domain.Entities.ImageExercise", "ImageExercise")
                        .WithMany("Exercises")
                        .HasForeignKey("ImageExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clean.Arch.Domain.Entities.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImageExercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Professional", b =>
                {
                    b.HasOne("Clean.Arch.Domain.Entities.IndividualEntity", "IndividualEntity")
                        .WithMany("Professional")
                        .HasForeignKey("IndividualEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IndividualEntity");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.User", b =>
                {
                    b.HasOne("Clean.Arch.Domain.Entities.IndividualEntity", "IndividualEntity")
                        .WithMany("User")
                        .HasForeignKey("IndividualEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clean.Arch.Domain.Entities.Login", "Login")
                        .WithMany()
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IndividualEntity");

                    b.Navigation("Login");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Workout", b =>
                {
                    b.HasOne("Clean.Arch.Domain.Entities.ImageExercise", null)
                        .WithMany("Workout")
                        .HasForeignKey("ImageExerciseId");

                    b.HasOne("Clean.Arch.Domain.Entities.IndividualEntity", "IndividualEntity")
                        .WithMany("Workout")
                        .HasForeignKey("IndividualEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Clean.Arch.Domain.Entities.Professional", "Personal")
                        .WithMany("Workout")
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("IndividualEntity");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.ImageExercise", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.IndividualEntity", b =>
                {
                    b.Navigation("Professional");

                    b.Navigation("User");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Professional", b =>
                {
                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Clean.Arch.Domain.Entities.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
