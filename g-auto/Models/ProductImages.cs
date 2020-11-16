using System.ComponentModel.DataAnnotations;

namespace g_auto.Models
{
    public class ProductImages
    {

        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}