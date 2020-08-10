using Models.Enums;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Text;
using Test.Models.UserModels;

namespace Services.Dto.MessageDto
{
    public class MsgBoxDTO
    {
        public List<Guid> To { get; set; }

        public List<Guid> Cc { get; set; }

        public Guid FromId { get; set; }

        public Guid Id { get; set; }

        public string FromFullName { get; set; }

        public string Subject { get; set; }

        public string Content { get; set; }

        public DateTime CreateOn { get; set; }

        public Guid CreatedById { get; set; }

        public User CreatedBy { get; set; }

        public string MessageNumber { get; set; }

        public string MessageCode { get; set; }

        public ImportanceLevel ImportanceLevel { get; set; }

        public Nullable<DateTime> DueDate { get; set; }

        //public Guid ResendOnId { get; set; }

        //public string ResendOnFullName { get; set; }

        //public List<MessageReciever> ResendTo { get; set; }

        public List<Guid> ResendToIdList { get; set; }
    }
}