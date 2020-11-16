using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Expert
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "This field cannot be empty.")]
        public int ExperienceYear { get; set; }
        [MaxLength(150)]
        public string ProfilePicture { get; set; }
        [NotMapped]
        public HttpPostedFileBase ProfilePictureFile { get; set; }
        public string FacebookProfile { get; set; }
        public string InstagramProfile { get; set; }
        public string LinkedInProfile { get; set; }
        public string TwitterProfile { get; set; }

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