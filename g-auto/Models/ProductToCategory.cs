namespace g_auto.Models
{
    public class ProductToCategory
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int ProductCategoryId { get; set; }

        public Product Product { get; set; }

        public ProductCategory ProductCategory { get; set; }

    }
}