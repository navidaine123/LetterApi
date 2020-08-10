using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Models.UserModels;

namespace Services.Dto.MessageDto
{
    public class MessageSenderDto
    {
        public Guid Id { get; set; }

        public string Prove { get; set; }

        public bool IsSent { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsMarked { get; set; }

        public MessageReciever ResendOn { get; set; }

        public virtual ICollection<MessageReciever> MessageRecievers { get; set; }

        public User User { get; set; }

        public Message Message { get; set; }
    }
}