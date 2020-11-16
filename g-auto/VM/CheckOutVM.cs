using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class CheckOutVM
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, MaxLength(150)]
        public string Address { get; set; }


        [MaxLength(500)]
        public string OrderNote { get; set; }

        public int[] ProductId { get; set; }

        public decimal[] ProductCount { get; set; }
        [MaxLength(14)]
        [Required]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?\s?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }
    }
}