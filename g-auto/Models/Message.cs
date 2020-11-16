using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class Message
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Name { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Subject { get; set; }
        [MaxLength(14)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?\s?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }
        public DateTime PostDate { get; set; }
        [Column(TypeName = "bit")]
        public bool isRead { get; set; }
        public Nullable<int> UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName = "bit")]
        public bool isReplied { get; set; }

        public List<MessageReply> MessageReplies { get; set; }
    }
}