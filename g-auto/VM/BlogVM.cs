using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class BlogVM
    {
        public List<Blog> Blogs { get; set; }
        public List<BlogCategory> Categories { get; set; }
        public List<Tags> Tags { get; set; }
        public Blog Blog { get; set; }
    }
}