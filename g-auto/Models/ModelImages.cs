using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class ModelImages
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
    }
}