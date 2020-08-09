using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto.MessageDto
{
    public class MessageSenderDto
    {
        public Guid Id { get; set; }

        public string Prove { get; set; }

        public bool IsSent { get; set; }

        public DateTime? DeletedDate { get; set; }

        public bool IsMarked { get; set; }

        public Nullable<Guid> ResendOnId { get; set; }

        public Guid UserId { get; set; }

        public Guid MessageId { get; set; }
    }
}