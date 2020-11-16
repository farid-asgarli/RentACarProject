using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class GalleryImage
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Title { get; set; }
   
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        public string Picture { get; set; }
        [NotMapped]
        public HttpPostedFileBase PictureFile { get; set; }

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