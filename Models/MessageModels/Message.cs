using Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Test.Models.UserModels;

namespace Models.MessageModels
{
    public class Message
    {
        [Key]
        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateOn { get; set; } = DateTime.UtcNow;

        public Guid CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public string MessageNumber { get; set; }

        public string MessageCode { get; set; }

        public ImportanceLevel ImportanceLevel { get; set; }

        public Nullable<DateTime> DueDate { get; set; }

        public List<MessageReciever> MessageRecievers { get; set; }

        public List<MessageSender> MessageSenders { get; set; }

        public MessageSender ReplyFrom { get; set; }
    }
}