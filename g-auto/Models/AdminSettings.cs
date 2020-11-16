using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class AdminSettings
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public Admin Admin { get; set; }
        [Column(TypeName = "bit")]
        public bool alwaysActive { get; set; }

        [MaxLength(125)]
        [Required]
        public string DisplayName { get; set; }

        [Column(TypeName = "bit")]
        public bool menuGrouping { get; set; }
    }
}