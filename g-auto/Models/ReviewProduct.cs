using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class ReviewProduct
    {
        public int Id { get; set; }

        [Column(TypeName = "ntext")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Content { get; set; }
        public DateTime PostedDate { get; set; }
        public User User { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public Product Product { get; set; }
        public Sale Sale { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }
        [ForeignKey("Sale")]
        public int SaleId { get; set; }

        [Range(0, 5)]
        public int Rating { get; set; }
        public List<ProductReviewReply> Replies { get; set; }
    }
}