﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Models.Migrations
{
    [DbContext(typeof(SmContext))]
    partial class SmContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Models.MessageModels.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Content")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("CreateOn")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("CreatedById")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<byte>("ImportanceLevel")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("MessageCode")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("MessageNumber")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Subject")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("Messages");
                });

            modelBuilder.Entity("Models.MessageModels.MessageReciever", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsCc")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsMarked")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MessageSenderId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("SeenDate")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("MessageSenderId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageRecievers");
                });

            modelBuilder.Entity("Models.MessageModels.MessageSender", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DeletedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<bool>("IsMarked")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("IsSent")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Prove")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("ResendOnId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("ResendOnId");

                    b.HasIndex("UserId");

                    b.ToTable("MessageSenders");
                });

            modelBuilder.Entity("Test.Models.UserModels.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int?>("DetailId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(30) CHARACTER SET utf8mb4")
                        .HasMaxLength(30);

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime>("LockoutEnd")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Mobile")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11);

                    b.Property<string>("NationalCode")
                        .IsRequired()
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("longblob");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("longblob");

                    b.Property<string>("Phone")
                        .HasColumnType("varchar(11) CHARACTER SET utf8mb4")
                        .HasMaxLength(11);

                    b.Property<string>("PmPasswordHash")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("PmUniqueId")
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4")
                        .HasMaxLength(36);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("varchar(50) CHARACTER SET utf8mb4")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            AccessFailedCount = 0,
                            FirstName = "نوید",
                            IsDeleted = false,
                            LastName = "آیینه وند",
                            LockoutEnabled = false,
                            LockoutEnd = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Mobile = "09361060437",
                            NationalCode = "98",
                            PasswordHash = new byte[] { 237, 164, 102, 64, 13, 103, 222, 102, 212, 29, 194, 153, 148, 121, 146, 72, 168, 201, 100, 68, 2, 54, 62, 94, 39, 57, 164, 103, 29, 194, 98, 241, 0, 217, 134, 193, 9, 100, 221, 195, 123, 46, 159, 10, 126, 96, 162, 33, 199, 131, 253, 191, 211, 75, 122, 238, 198, 233, 32, 129, 125, 30, 12, 222 },
                            PasswordSalt = new byte[] { 55, 95, 77, 66, 127, 54, 214, 53, 22, 202, 46, 72, 46, 117, 47, 0, 187, 184, 32, 12, 158, 255, 202, 123, 110, 18, 183, 21, 217, 44, 124, 90, 10, 52, 68, 146, 27, 126, 167, 125, 113, 210, 145, 211, 59, 218, 113, 197, 203, 134, 135, 213, 255, 191, 150, 81, 229, 65, 122, 79, 118, 21, 13, 23, 132, 245, 220, 84, 211, 45, 178, 125, 2, 239, 10, 100, 38, 98, 55, 95, 20, 210, 13, 223, 80, 130, 41, 177, 158, 46, 29, 34, 24, 126, 149, 200, 216, 215, 240, 137, 58, 218, 113, 26, 125, 165, 58, 20, 126, 14, 109, 177, 145, 24, 9, 145, 28, 210, 253, 135, 220, 204, 160, 105, 213, 90, 73, 14 },
                            UserName = "navid"
                        });
                });

            modelBuilder.Entity("Models.MessageModels.Message", b =>
                {
                    b.HasOne("Test.Models.UserModels.User", "CreatedBy")
                        .WithMany()
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.MessageModels.MessageReciever", b =>
                {
                    b.HasOne("Models.MessageModels.Message", "Message")
                        .WithMany("MessageRecievers")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.MessageModels.MessageSender", "MessageSender")
                        .WithMany("MessageRecievers")
                        .HasForeignKey("MessageSenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Test.Models.UserModels.User", "User")
                        .WithMany("MessageRecievers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Models.MessageModels.MessageSender", b =>
                {
                    b.HasOne("Models.MessageModels.Message", "Message")
                        .WithMany("MessageSenders")
                        .HasForeignKey("MessageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Models.MessageModels.MessageReciever", "ResendOn")
                        .WithMany("ResentMessages")
                        .HasForeignKey("ResendOnId");

                    b.HasOne("Test.Models.UserModels.User", "User")
                        .WithMany("MessageSenders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
