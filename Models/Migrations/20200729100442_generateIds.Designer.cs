﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Models.Migrations
{
    [DbContext(typeof(SmContext))]
    [Migration("20200729100442_generateIds")]
    partial class generateIds
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<bool>("IsCc")
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
                            PasswordHash = new byte[] { 187, 182, 220, 151, 65, 217, 94, 153, 178, 54, 232, 203, 23, 240, 179, 250, 142, 76, 114, 234, 227, 132, 251, 151, 63, 231, 135, 191, 113, 135, 168, 206, 110, 71, 101, 50, 188, 241, 25, 228, 5, 210, 173, 148, 164, 255, 223, 156, 92, 27, 145, 241, 145, 60, 12, 101, 157, 253, 9, 21, 64, 225, 74, 239 },
                            PasswordSalt = new byte[] { 7, 140, 219, 236, 175, 73, 96, 120, 8, 86, 208, 179, 216, 55, 112, 142, 119, 3, 172, 29, 4, 66, 55, 140, 122, 142, 198, 90, 103, 3, 82, 41, 255, 187, 97, 35, 79, 141, 103, 92, 4, 40, 54, 204, 79, 122, 144, 20, 90, 225, 131, 192, 145, 240, 161, 49, 115, 136, 49, 223, 166, 71, 130, 248, 226, 211, 221, 17, 238, 84, 124, 227, 26, 194, 184, 211, 90, 109, 76, 246, 13, 27, 19, 196, 170, 87, 162, 91, 99, 190, 179, 71, 211, 228, 153, 222, 98, 120, 30, 210, 30, 234, 166, 53, 212, 22, 9, 0, 203, 178, 238, 188, 44, 29, 191, 230, 76, 213, 92, 99, 123, 223, 161, 234, 64, 132, 205, 102 },
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
                        .WithMany()
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
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
