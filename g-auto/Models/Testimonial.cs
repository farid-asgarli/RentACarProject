using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class Testimonial
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
    }
}