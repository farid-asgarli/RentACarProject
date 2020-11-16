using g_auto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class ServiceReservationVM
    {
        public int ServiceId { get; set; }
        public int UserId { get; set; }
        [Required]
        public DateTime AppDate { get; set; }
        [Required]
        public DateTime Time { get; set; }

        [MaxLength(250)]
        [Required]
        public string FullName { get; set; }


        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [MaxLength(500)]
        public string Message { get; set; }

        [MaxLength(14)]
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?\s?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

    }
}