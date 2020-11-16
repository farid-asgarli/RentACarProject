using g_auto.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class ReservationVM
    {
        public List<FeatureSet> FeatureSets { get; set; }
        public Model Model { get; set; }
        [MaxLength(500)]
        [Required]
        public string DeliveryAddress { get; set; }

        public int ModelId { get; set; }
        public int[] FeatureSetId { get; set; }

        public string[] isIncluded { get; set; }

        [Required]

        public DateTime StartDate { get; set; }

        //[DisplayName("End Date")]
        //[DataType(DataType.Date)]
        [Required]

        public DateTime EndDate { get; set; }

        //[DisplayName("Time")]
        //[DataType(DataType.Time)]
        [Required]

        public DateTime Time { get; set; }


        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal ModelPrice { get; set; }

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
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        [Required]
        [RegularExpression(@"^\(?([0-9]{3})\)?\s?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Phone { get; set; }

        public List<Reservation> Reservations { get; set; }

    }
}