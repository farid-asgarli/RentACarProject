using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        [Display(Name = "Email address")]
       
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        public string ProfilePicture { get; set; }
        [MaxLength(14)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?\s?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        public string Speciality { get; set; }

        [NotMapped]
        public HttpPostedFileBase ProfilePictureFile { get; set; }
        public List<AdminSettings> AdminSettings { get; set; }

        public List<MessageReply> MessageReplies { get; set; }

        [Column(TypeName = "bit")]
        public bool hasPrivelege { get; set; }

       

        [Column(TypeName = "bit")]
        public bool isBlocked { get; set; }

        [Column(TypeName = "bigint")]
        public long EntranceCount { get; set; }

        public Nullable<DateTime> LastEntranceTime { get; set; }
        public string LastIPAddress { get; set; }

        [MaxLength(100)]
        public string ConnectionId { get; set; }
    }
}