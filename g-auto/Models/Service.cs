using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Service
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Title { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        [NotMapped]
        public string[] Benefits { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Description { get; set; }
        public string ServiceImageFirst { get; set; }
        [NotMapped]
        public HttpPostedFileBase ServiceImageFirstFile { get; set; }
        public string ServiceImageSecond { get; set; }
        [NotMapped]
        public HttpPostedFileBase ServiceImageSecondFile { get; set; }
        public string ServiceIcon { get; set; }
        [NotMapped]
        public HttpPostedFileBase ServiceIconFile { get; set; }

        [NotMapped]
        public int[] ServiceInfoId { get; set; }

        public List<ServiceToInfo> ServiceToInfo { get; set; }

        public List<ServiceBenefit> ServiceBenefits { get; set; }
        public List<ReservationService> ReservationServices { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public DateTime PostDate { get; set; }
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }

        [Column(TypeName = "bigint")]
        public long ViewCount { get; set; }
    }
}