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
    [Migration("20200819124539_replyToInMessageSenderRelationWithReciever4")]
    partial class replyToInMessageSenderRelationWithReciever4
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

                    b.Property<Guid?>("ReplyToId")
                        .HasColumnType("char(36)");

                    b.Property<Guid?>("ResendOnId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("MessageId");

                    b.HasIndex("ReplyToId");

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
                            PasswordHash = new byte[] { 251, 146, 100, 49, 96, 32, 33, 123, 210, 144, 117, 243, 211, 230, 244, 154, 205, 242, 143, 107, 44, 220, 16, 189, 46, 67, 231, 57, 100, 205, 226, 153, 140, 16, 185, 199, 248, 167, 121, 129, 36, 38, 29, 141, 37, 129, 190, 144, 217, 81, 130, 83, 230, 231, 177, 67, 180, 29, 6, 231, 137, 1, 206, 103 },
                            PasswordSalt = new byte[] { 200, 170, 134, 129, 3, 205, 251, 177, 21, 216, 177, 32, 119, 125, 68, 132, 173, 232, 75, 69, 122, 222, 211, 53, 0, 124, 192, 251, 38, 133, 105, 195, 175, 139, 70, 94, 15, 195, 39, 100, 218, 24, 104, 16, 13, 162, 90, 76, 76, 48, 177, 74, 185, 205, 217, 81, 147, 213, 25, 221, 204, 154, 101, 48, 128, 207, 68, 116, 42, 247, 171, 23, 218, 107, 186, 29, 156, 247, 166, 13, 143, 181, 144, 33, 107, 96, 1, 3, 214, 107, 108, 159, 74, 155, 101, 105, 139, 206, 116, 205, 199, 131, 178, 14, 92, 126, 169, 180, 34, 112, 134, 123, 236, 148, 163, 217, 83, 234, 233, 157, 111, 169, 13, 82, 8, 50, 173, 220 },
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
                        .OnDelete(DeleteBehavior.NoAction)
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

                    b.HasOne("Models.MessageModels.MessageReciever", "ReplyTo")
                        .WithMany("ReplyFrom")
                        .HasForeignKey("ReplyToId")
                        .OnDelete(DeleteBehavior.NoAction);

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
