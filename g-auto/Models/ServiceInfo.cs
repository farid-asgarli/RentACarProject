using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class ServiceInfo
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        [MaxLength(120)]
        public string Title { get; set; }
        public List<ServiceToInfo> ServiceToInfo { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public DateTime PostDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
    }
}