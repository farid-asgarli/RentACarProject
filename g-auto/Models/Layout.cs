using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class Layout
    {
        public int Id { get; set; }
        [Column(TypeName = "bit")]
        public bool sliderToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool modelToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool serviceToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool testimonialToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool blogToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool expertToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool promoToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool aboutToggle { get; set; }
        [Column(TypeName = "bit")]
        public bool contactToggle { get; set; }
        public int? sliderCount { get; set; }
        public int? blogCount { get; set; }
        public int? serviceCount { get; set; }
        public int? modelCount { get; set; }
        public int? expertCount { get; set; }
        public int? testiominalCount { get; set; }
        public string Logo { get; set; }
        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }
        public string LogoFooter { get; set; }
        [NotMapped]
        public HttpPostedFileBase LogoFooterFile { get; set; }
        [MaxLength(150)]
        public string Promo_Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase Promo_ImageFile { get; set; }

        [MaxLength(150)]
        public string About_Image { get; set; }

        [NotMapped]
        public HttpPostedFileBase About_ImageFile { get; set; }
        [MaxLength(150)]
        public string Signature { get; set; }

        [NotMapped]
        public HttpPostedFileBase SignatureFile { get; set; }
        [MaxLength(150)]
        public string About_Title { get; set; }
        [Column(TypeName = "ntext")]
        public string About_Content { get; set; }
        [MaxLength(150)]
        public string About_CEO { get; set; }

        [MaxLength(150)]
        public string Contact_Title { get; set; }
        [Column(TypeName = "ntext")]
        public string Contact_Content { get; set; }

        [Column(TypeName = "ntext")]
        public string Promo_Content { get; set; }
        [MaxLength(500)]
        public string Promo_Link { get; set; }

        [NotMapped]
        public string[] Benefits { get; set; }
        public List<AboutBenefits> AboutBenefits { get; set; }
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

    }
}