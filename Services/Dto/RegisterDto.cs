using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Services.Dto
{
    public class RegisterDto
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(10), MaxLength(10)]
        public string NationalCode { get; set; }

        [MinLength(11), MaxLength(11)]
        public string Mobile { get; set; }

        [MinLength(11), MaxLength(11)]
        public string Phone { get; set; }

        public string Password { get; set; }
    }
}