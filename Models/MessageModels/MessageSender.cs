using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Test.Models.UserModels;

namespace Models.MessageModels
{
    public class MessageSender
    {
        [Key]
        public Guid Id { get; set; }

        public string Prove { get; set; }

        public bool IsSent { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsMarked { get; set; }

        public Nullable<Guid> ResendOnId { get; set; }

        public MessageReciever ResendOn { get; set; }

        public virtual ICollection<MessageReciever> MessageRecievers { get; set; }

        public Guid UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Message")]
        public Guid MessageId { get; set; }

        public Message Message { get; set; }
    }
}