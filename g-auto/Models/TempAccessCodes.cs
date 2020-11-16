using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class TempAccessCodes
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Access code cannot be empty.")]
        [MaxLength(128)]
        public string Code { get; set; }
        public DateTime PostDate { get; set; }
    }
}