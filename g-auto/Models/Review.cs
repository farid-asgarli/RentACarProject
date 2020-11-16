using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public User User { get; set; }
        public Reservation Reservation { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public int ReservationId { get; set; }
        public Model Model { get; set; }

        [ForeignKey("Model")]
        public int ModelId { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
        public List<ModelReviewReply> Replies { get; set; }
    }
}