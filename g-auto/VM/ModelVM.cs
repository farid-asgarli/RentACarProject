using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class ModelVM
    {
        public List<Brand> Brands { get; set; }
        public List<Model> Models { get; set; }
        public Model Model { get; set; }
    }
}