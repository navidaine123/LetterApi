using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Models.UserModels;

namespace Models.Mapping
{
    public class MessageSenderMapping : IEntityTypeConfiguration<MessageSender>
    {
        public void Configure(EntityTypeBuilder<MessageSender> builder)
        {
            builder
                .HasMany(p => p.MessageRecievers)
                .WithOne(p => p.MessageSender)
                .HasForeignKey(p => p.MessageSenderId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }

    public class MessageRecieverMapping : IEntityTypeConfiguration<MessageReciever>
    {
        public void Configure(EntityTypeBuilder<MessageReciever> builder)
        {
            builder
                .HasMany(p => p.ResentMessages)
                .WithOne(p => p.ResendOn)
                .HasForeignKey(p => p.ResendOnId);
        }
    }
}