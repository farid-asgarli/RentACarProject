using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class ServiceBenefit
    {
        public int Id { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }

        public int ServiceId { get; set; }

        public Service Service { get; set; }

    }
}