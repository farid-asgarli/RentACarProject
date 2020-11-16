using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class ReviewVM
    {
        public List<Review> Reviews { get; set; }
        public List<ReviewProduct> ReviewProducts { get; set; }
    }
}