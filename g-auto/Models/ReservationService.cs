using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class ReservationService
    {

        public int Id { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }


        [Column(TypeName = "bit")]
        public bool isPending { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        [Column(TypeName = "bit")]
        public bool isFinished { get; set; }
        [Column(TypeName = "bit")]
        public bool isCancelled { get; set; }

        [DisplayName("Appointment Date")]
        [DataType(DataType.Date)]
        public DateTime AppDate { get; set; }

        [DisplayName("Time")]
        [DataType(DataType.Time)]
        public DateTime Time { get; set; }

        public DateTime PostDate { get; set; }
    }
}