using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; }


        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
        public DateTime PostDate { get; set; }
        [MaxLength(150)]
        public string BlogCoverImage { get; set; }
        [NotMapped]
        public HttpPostedFileBase BlogCoverImageFile { get; set; }
        public List<Comment> Comments { get; set; }
        public List<Reply> Replies { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Description { get; set; }
        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Title { get; set; }

        public List<BlogToCategory> BlogToCategory { get; set; }
        [NotMapped]
        public int[] BlogCategoryId { get; set; }

        public List<BlogToTags> BlogToTags { get; set; }
        [NotMapped]
        public int[] TagId { get; set; }
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        [Column(TypeName = "bit")]
        public bool enableComments { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        [Column(TypeName ="bigint")]
        public long ViewCount { get; set; }

    }
}