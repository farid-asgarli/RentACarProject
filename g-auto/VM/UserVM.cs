using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class UserVM
    {
        public User User { get; set; }
        public List<Message> Messages { get; set; }
    }
}