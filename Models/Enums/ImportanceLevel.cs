using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Enums
{
    public enum ImportanceLevel : byte
    {
        [Display(Name = "عادی")]
        Normal = 0,

        [Display(Name = "مهم")]
        Important = 1,

        [Display(Name = "محرمانه")]
        Confidential = 2,

        [Display(Name = "فوری")]
        Urgent = 3,
    }
}