using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class VerificationCode
    {
        public int Id { get; set; }
        public DateTime PostDate { get; set; }
        [MaxLength(128)]
        public string VerCode { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}