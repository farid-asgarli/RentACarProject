using g_auto.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace g_auto.ViewModels
{
    public class Dashboard
    {
        public List<Admin> Admins { get; set; }
        public Admin Admin { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Product> Products { get; set; }
        public List<ReservationService> Appointments { get; set; }
        public List<Model> Models { get; set; }
        public List<Reservation> Reservations { get; set; }

        public List<Blog> ModifiedBlogs { get; set; }
        public List<Brand> ModifiedBrands { get; set; }
        public List<Expert> ModifiedExperts { get; set; }
        public List<GalleryImage> ModifiedGallery { get; set; }
        public List<Model> ModifiedModels { get; set; }
        public List<Product> ModifiedProducts { get; set; }
        public List<Service> ModifiedServices { get; set; }
        public List<Sliders> ModifiedSlider { get; set; }

        public List<Blog> Blogs { get; set; }
        public List<Brand> Brands { get; set; }
        public List<Expert> Experts { get; set; }
        public List<GalleryImage> Gallery { get; set; }
        public List<Service> Services { get; set; }
        public List<Sliders> Sliders { get; set; }
    }
}