using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace g_auto.Models
{
    public class Reservation
    {
        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ModelId {get;set;}

        public Model Model { get; set; }


        [Column(TypeName = "bit")]
        public bool isPending { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        [Column(TypeName = "bit")]
        public bool isFinished { get; set; }

        [Column(TypeName = "bit")]
        public bool isCancelled { get; set; }
        [MaxLength(500)]
        [Required]
        public string DeliveryAddress { get; set; }

        [DisplayName("Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        public DateTime PostDate { get; set; }

        [DisplayName("End Date")]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        [DisplayName("Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public Admin Admin { get; set; }
        [ForeignKey("Admin")]
        public Nullable<int> CancelledAdminId { get; set; }
        public Nullable<DateTime> CancelledDate { get; set; }
        public List<ReservationToFeatures> ReservationToFeatures { get; set; }
        public List<Review> Reviews { get; set; }

    }
}