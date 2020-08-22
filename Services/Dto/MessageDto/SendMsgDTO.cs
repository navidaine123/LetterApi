using Models.Enums;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Models.UserModels;

namespace Services.Dto.MessageDto
{
    public class SendMsgDTO
    {
        public List<Guid> To { get; set; }

        public List<Guid> Cc { get; set; }

        public Guid CreatedById { get; set; }

        public Guid Id { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateOn { get; set; } = DateTime.UtcNow;

        public string MessageNumber { get; set; }

        public ImportanceLevel ImportanceLevel { get; set; }

        public Nullable<DateTime> DueDate { get; set; }

        public Guid MessageSendersId { get; set; }
    }
}