namespace g_auto.Models
{
    public class BlogToCategory
    {
        public int Id { get; set; }

        public int BlogId { get; set; }

        public int BlogCategoryId { get; set; }

        public Blog Blog { get; set; }

        public BlogCategory BlogCategory { get; set; }
    }
}