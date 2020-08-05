using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Helpers
{
    public enum ResponseMessage
    {
        [Display(Name = "عملیات با موفقیت انجام شد")]
        Ok = 0,

        [Display(Name = "عملیات با خطا مواجه شد")]
        BadRequest = 1,

        [Display(Name = "کاربر باید وارد شود")]
        NotAuthentication = 2,

        [Display(Name = "مقادیر وارد شده اشتباه است")]
        InvalidValue = 3,

        [Display(Name = "مقادیر باید وارد شوند")]
        NoValue = 4,
    }
}