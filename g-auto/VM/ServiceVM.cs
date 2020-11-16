using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class ServiceVM
    {
       public List<Service> Services { get; set; }
       public Service Service { get; set; }
    }
}