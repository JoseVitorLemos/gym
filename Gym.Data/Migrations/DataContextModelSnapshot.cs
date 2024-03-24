﻿// <auto-generated />
using System;
using Gym.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Gym.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Gym.Domain.Entities.Exercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

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

            modelBuilder.Entity("Gym.Domain.Entities.ImageExercise", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

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

                    b.ToTable("ImageExercises", (string)null);
                });

            modelBuilder.Entity("Gym.Domain.Entities.IndividualEntity", b =>
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

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Gender")
                        .HasMaxLength(2)
                        .HasColumnType("int");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Cpf")
                        .IsUnique();

                    b.HasIndex("LoginId");

                    b.ToTable("IndividualEntities", (string)null);
                });

            modelBuilder.Entity("Gym.Domain.Entities.Login", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Logins", (string)null);
                });

            modelBuilder.Entity("Gym.Domain.Entities.LoginConfirmation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.Property<DateTime?>("ConfirmedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<bool>("EmailConfirmation")
                        .HasColumnType("bit");

                    b.Property<Guid>("LoginId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("LoginId");

                    b.ToTable("LoginConfirmations", (string)null);
                });

            modelBuilder.Entity("Gym.Domain.Entities.Professional", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Cref")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("IndividualEntityId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("IndividualEntityId")
                        .IsUnique();

                    b.ToTable("PROFESSIONALS", (string)null);
                });

            modelBuilder.Entity("Gym.Domain.Entities.Workout", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Division")
                        .HasColumnType("int");

                    b.Property<Guid>("FitnessClientId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ImageExerciseId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PersonalId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("FitnessClientId");

                    b.HasIndex("ImageExerciseId");

                    b.HasIndex("PersonalId");

                    b.ToTable("WORKOUTS", (string)null);
                });

            modelBuilder.Entity("Gym.Domain.Entities.Exercise", b =>
                {
                    b.HasOne("Gym.Domain.Entities.ImageExercise", "ImageExercise")
                        .WithMany("Exercises")
                        .HasForeignKey("ImageExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gym.Domain.Entities.Workout", "Workout")
                        .WithMany("Exercises")
                        .HasForeignKey("WorkoutId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ImageExercise");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Gym.Domain.Entities.IndividualEntity", b =>
                {
                    b.HasOne("Gym.Domain.Entities.Login", "Login")
                        .WithMany("IndividualEntity")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("Gym.Domain.Entities.LoginConfirmation", b =>
                {
                    b.HasOne("Gym.Domain.Entities.Login", "Login")
                        .WithMany("LoginConfirmation")
                        .HasForeignKey("LoginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Login");
                });

            modelBuilder.Entity("Gym.Domain.Entities.Professional", b =>
                {
                    b.HasOne("Gym.Domain.Entities.IndividualEntity", "IndividualEntity")
                        .WithMany("Professional")
                        .HasForeignKey("IndividualEntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IndividualEntity");
                });

            modelBuilder.Entity("Gym.Domain.Entities.Workout", b =>
                {
                    b.HasOne("Gym.Domain.Entities.IndividualEntity", "IndividualEntity")
                        .WithMany("Workout")
                        .HasForeignKey("FitnessClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Gym.Domain.Entities.ImageExercise", null)
                        .WithMany("Workout")
                        .HasForeignKey("ImageExerciseId");

                    b.HasOne("Gym.Domain.Entities.Professional", "Personal")
                        .WithMany("Workout")
                        .HasForeignKey("PersonalId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("IndividualEntity");

                    b.Navigation("Personal");
                });

            modelBuilder.Entity("Gym.Domain.Entities.ImageExercise", b =>
                {
                    b.Navigation("Exercises");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Gym.Domain.Entities.IndividualEntity", b =>
                {
                    b.Navigation("Professional");

                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Gym.Domain.Entities.Login", b =>
                {
                    b.Navigation("IndividualEntity");

                    b.Navigation("LoginConfirmation");
                });

            modelBuilder.Entity("Gym.Domain.Entities.Professional", b =>
                {
                    b.Navigation("Workout");
                });

            modelBuilder.Entity("Gym.Domain.Entities.Workout", b =>
                {
                    b.Navigation("Exercises");
                });
#pragma warning restore 612, 618
        }
    }
}
