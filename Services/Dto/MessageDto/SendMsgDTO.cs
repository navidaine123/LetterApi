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

        public Guid FromId { get; set; }

        public Guid Id { get; set; }

        public string FromFullName { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateOn { get; set; } = DateTime.UtcNow;

        public Guid CreatedById { get; set; }

        public string CreatedByFullName { get; set; }

        public string MessageNumber { get; set; }

        public string MessageCode { get; set; }

        public ImportanceLevel ImportanceLevel { get; set; }

        public Nullable<DateTime> DueDate { get; set; }
    }
}