using Models.Enums;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto.MessageDto
{
    public class ReplyMessageDTO
    {
        public Guid Id { get; set; }

        public Guid MessageCreatedById { get; set; }

        public Guid MessageRecieversUserId { get; set; }

        public string MessageSubject { get; set; }

        public string MessageMessageNumber { get; set; }

        public DateTime MessageCreateOn { get; set; }

        public string MessageContent { get; set; }

        public ImportanceLevel MessageImportanceLevel { get; set; }

        public Nullable<DateTime> MessageDueDate { get; set; }
    }
}