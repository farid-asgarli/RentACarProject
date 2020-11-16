using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class FeatureSet
    {
        public int Id { get; set; }
        [MaxLength(125)]
        [Required]
        public string Title { get; set; }
        [Required]
        [MaxLength(250)]
        public string Content { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public decimal Price { get; set; }

        public DateTime PostDate { get; set; }
        public List<ReservationToFeatures> ReservationToFeatures { get; set; }

        public Nullable<DateTime> ModifiedDate { get; set; }


        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}