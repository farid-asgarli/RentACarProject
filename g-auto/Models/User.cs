using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$")]
        public string FullName { get; set; }
        [MaxLength(500)]
        public string Address { get; set; }
        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
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

        [NotMapped]
        public HttpPostedFileBase ProfilePictureFile { get; set; }
        public List<Reply> Replies { get; set; }
       
        public List<Comment> Comments { get; set; }
        public List<Testimonial> Testimonials { get; set; }

        [Column(TypeName = "bit")]
        public bool IsRegistered { get; set; }
        [Column(TypeName = "bit")]
        public bool isBlocked { get; set; }
        public List<Sale> Sales { get; set; }
        public DateTime PostDate { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<ReviewProduct> ReviewProducts { get; set; }
        public List<Review> Reviews { get; set; }
        public List<ReservationService> ReservationServices { get; set; }

        [Column(TypeName = "bigint")]
        public long EntranceCount { get; set; }

        public Nullable<DateTime> LastEntranceTime { get; set; }
        public string LastIPAddress { get; set; }
        [MaxLength(100)]
        public string ConnectionId { get; set; }
    }
}