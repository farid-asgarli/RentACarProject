using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class AboutBenefits
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Content { get; set; }

        public int LayoutId { get; set; }

        public Layout Layout{ get; set; }

    }
}