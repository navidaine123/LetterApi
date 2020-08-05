//using sm.Data.Enums;
//using sm.Data.Models.FileModels;
//using sm.Data.Models.ProjectModels;
using Models.MessageModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;

namespace Test.Models.UserModels
{
    public class User
    {
        public User()
        {
            //UserRoles = new Collection<UserRole>();
            //PmPassword = MyEncription.StringCipher.CreateMD5(DateTime.Now.Ticks + "").Substring(0, 20).Trim();
        }

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

        //public CenterName NameOfCenter { get; set; }

        public Nullable<int> DetailId { get; set; }

        public bool IsDeleted { get; set; }

        //public UserStatus Status { get; set; }

        //public UserDetail Detail { get; set; }

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        //public Guid ClaimJwtId { get; set; }
        //public ClaimJwt ClaimJwt { get; set; }

        //public virtual ICollection<Department> Departments { get; set; }

        //public virtual IList<UserRole> UserRoles { get; set; }

        //public virtual IList<UserFile> UserFiles { get; set; }

        //readonly
        public string FullName { get { return FirstName + " " + LastName; } }

        public bool LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public List<MessageSender> MessageSenders { get; set; }

        public List<MessageReciever> MessageRecievers { get; set; }

        public DateTime LockoutEnd { get; set; }
        //public virtual IList<Project> Projects { get; set; }

        //Pm Fields
        [MaxLength(36)]
        public string PmUniqueId { get; set; }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public string PmPasswordHash { get; set; }

        //[NotMapped]
        //public string PmPassword
        //{
        //    get
        //    {
        //        try
        //        {
        //            return MyEncription.StringCipher.Decrypt(PmPasswordHash, "pm-user-key-for-hash");
        //        }
        //        catch
        //        {
        //            PmPassword = MyEncription.StringCipher.CreateMD5(DateTime.Now.Ticks + "").Substring(0, 20).Trim();
        //            return PmPassword;
        //        }
        //    }
        //    set
        //    {
        //        PmPasswordHash = MyEncription.StringCipher.Encrypt(value, "pm-user-key-for-hash");
        //    }
        //}
    }
}