using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Test.Models.UserModels;

namespace Models.MessageModels
{
    public class MessageReciever
    {
        [Key]
        public Guid Id { get; set; }

        public DateTime? SeenDate { get; set; }

        public bool IsCc { get; set; }

        public Nullable<DateTime> DeletedDate { get; set; }

        public bool IsMarked { get; set; }

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User User { get; set; }

        [ForeignKey("Message")]
        public Guid MessageId { get; set; }

        public Message Message { get; set; }

        public Guid MessageSenderId { get; set; }

        public MessageSender MessageSender { get; set; }

        public virtual ICollection<MessageSender> ResentMessages { get; set; }

        public List<MessageSender> ReplyFrom { get; set; }
    }
}