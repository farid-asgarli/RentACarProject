using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class Vacancy
    {
        public int Id { get; set; }
        [Column(TypeName = "bigint")]
        public long ViewCount { get; set; }

        [ForeignKey("Admin")]
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
        public DateTime PostDate { get; set; }
        [MaxLength(150)]
        public string CoverImage { get; set; }
        [NotMapped]
        public HttpPostedFileBase CoverImageFile { get; set; }
        [MaxLength(250)]
        public string Title { get; set; }

        [MaxLength(250),Required(ErrorMessage ="Salary can not be empty.")]
        public string Salary { get; set; }
        [Column(TypeName ="ntext")]
        public string Content { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
    }
}