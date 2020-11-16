using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class About
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Title { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Content { get; set; }

        [MaxLength(14)]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "You must provide a phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?\s?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(150)]
        [Required]
        public string Address { get; set; }
        public string FacebookLink { get; set; }
        public string LinkedinLink { get; set; }
        public string InstagramLink { get; set; }
        public string VimeoLink { get; set; }
        public string TwitterLink { get; set; }
        public string PinterestLink { get; set; }
        public string SkypeLink { get; set; }
        [MaxLength(150)]
        public string CoverImageFirst { get; set; }
        [MaxLength(50)]
        public string CoverImageFirstTitle { get; set; }
        [MaxLength(50)]
        public string CoverImageSecondTitle { get; set; }
        [MaxLength(50)]
        public string CoverImageThirdTitle { get; set; }

        [MaxLength(150)]
        public string CoverImageFirstContent { get; set; }
        [MaxLength(150)]
        public string CoverImageSecondContent { get; set; }
        [MaxLength(150)]
        public string CoverImageThirdContent { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageFirstFile { get; set; }
        [MaxLength(150)]
        public string CoverImageSecond { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageSecondFile { get; set; }
        [MaxLength(150)]
        public string CoverImageThird { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageThirdFile { get; set; }
        [MaxLength(150)]
        public string CoverImageFourth { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageFourthFile { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public DateTime PostDate{ get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        public Nullable<DateTime> ModifiedDate { get; set; }


        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
    }
}