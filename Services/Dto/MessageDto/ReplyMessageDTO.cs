using Models.Enums;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto.MessageDto
{
    public class ReplyMessageDTO
    {
        public Guid MessageSenderId { get; set; }

        public Guid MessageRecieverId { get; set; }

        public string MessageCreatedByFullName { get; set; }

        public string MessageRecieverUserFullName { get; set; }

        public string MessageSubject { get; set; }

        public string MessageMessageNumber { get; set; }

        public DateTime MessageCreateOn { get; set; }

        public string MessageContent { get; set; }

        public ImportanceLevel MessageImportanceLevel { get; set; }

        public Nullable<DateTime> MessageDueDate { get; set; }
    }
}