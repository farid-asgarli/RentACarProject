namespace g_auto.Models
{
    public class BlogToTags
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int TagId { get; set; }

        public Blog Blog { get; set; }

        public Tags Tag { get; set; }
    }
}