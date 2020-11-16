using g_auto.Models;
using System.Data.Entity;

namespace g_auto.DAL
{
    public class DBContext : DbContext
    {
        public DBContext() : base("gautoCon")
        {

        }

        public DbSet<About> About { get; set; }
        public DbSet<AboutBenefits> AboutBenefits { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AdminSettings> AdminSettings { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogToCategory> BlogToCategory { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogToTags> BlogToTags { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Expert> Expert { get; set; }
        public DbSet<FeatureSet> FeatureSet { get; set; }
        public DbSet<GalleryImage> GalleryImage { get; set; }
        public DbSet<Layout> Layout { get; set; }
        public DbSet<Message> Message { get; set; }
        public DbSet<MessageReply> MessageReplies { get; set; }
        public DbSet<Model> Model { get; set; }
        public DbSet<ModelImages> ModelImages { get; set; }
        public DbSet<ModelReviewReply> ModelReviewReplies { get; set; }
        public DbSet<NewsLetter> NewsLetters { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductReviewReply> ProductReviewReplies { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductToCategory> ProductToCategory { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<ReviewProduct> ReviewProduct { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationService> ReservationServices { get; set; }
        public DbSet<ReservationToFeatures> ReservationToFeatures { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<ServiceBenefit> ServiceBenefits { get; set; }
        public DbSet<ServiceInfo> ServiceInfo { get; set; }
        public DbSet<ServiceToInfo> ServiceToInfo { get; set; }
        public DbSet<Shipment> Shipments { get; set; }
        public DbSet<Sliders> Sliders { get; set; }
        public DbSet<Tags> Tags { get; set; }
        public DbSet<Testimonial> Testimonials { get; set; }
        public DbSet<TempAccessCodes> TempAccessCodes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<VerificationCode> VerificationCodes { get; set; }
    }
}