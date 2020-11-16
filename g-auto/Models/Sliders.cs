using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class Sliders
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Link { get; set; }
        [MaxLength(150)]
        public string Image { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImageFile { get; set; }

        public int? Order { get; set; }
        public DateTime PostDate { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
        public int AdminId { get; set; }

        public Admin Admin { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
    }
}