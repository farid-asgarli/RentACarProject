using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Name { get; set; }
        [MaxLength(150)]

        public string Logo { get; set; }

        [NotMapped]
        public HttpPostedFileBase LogoFile { get; set; }
        public List<Model> Models { get; set; }

        public string OriginCountry { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public DateTime PostDate { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
    }
}