using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Dto.MessageDto
{
    public class ForwardMsgDto
    {
        public Guid MessageRecieverId { get; set; }

        public Guid[] ToIds { get; set; }

        public string Prove { get; set; }
    }
}