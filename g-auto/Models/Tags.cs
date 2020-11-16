using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class Tags
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; }

        public List<BlogToTags> BlogToTags { get; set; }

        public int AdminId { get; set; }
        [ForeignKey("AdminId")]

        public Admin Admin { get; set; }

        public DateTime PostDate { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }
        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
    }
}