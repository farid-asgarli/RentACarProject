using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.VM
{
    public class Home
    {
        public List<Sliders> Sliders { get; set; }
        public Layout Layout { get; set; }
        public List<Service> Services { get; set; }
        public List<Expert> Experts { get; set; }
        public About About { get; set; }
        public List<Model> Models { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<Testimonial> Testimonials { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<User> Users { get; set; }
        public List<Sale> Sales { get; set; }
   
        public List<ReservationService> Appointments { get; set; }
    }
}