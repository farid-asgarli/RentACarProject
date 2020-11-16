using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class ProductVM
    {
        public List<Product> Products { get; set; }
        public List<ProductCategory> Categories { get; set; }
        public Product Product { get; set; }
    }
}