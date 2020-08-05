using Helpers.Common;
using Microsoft.EntityFrameworkCore;
using Models.Mapping;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Test.Models.UserModels;

namespace Models
{
    public class SmContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<MessageReciever> MessageRecievers { get; set; }

        public DbSet<MessageSender> MessageSenders { get; set; }

        public SmContext(DbContextOptions<SmContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UtilityHelper.CreatePasswordHash("313", out byte[] defaultPass, out byte[] defaultSalt);

            var user1 = new User
            {
                Id = new Guid("00000000-0000-0000-0000-000000000001"),
                FirstName = "نوید",
                LastName = "آیینه وند",
                UserName = "navid",
                AccessFailedCount = 0,
                IsDeleted = false,
                LockoutEnabled = false,
                Mobile = "09361060437",
                NationalCode = "98",
                PasswordSalt = defaultSalt,
                PasswordHash = defaultPass,
            };
            modelBuilder.Entity<User>()
                .HasData(user1);

            #region Mapping

            modelBuilder.ApplyConfiguration(new MessageRecieverMapping());
            modelBuilder.ApplyConfiguration(new MessageSenderMapping());

            #endregion Mapping
        }
    }
}