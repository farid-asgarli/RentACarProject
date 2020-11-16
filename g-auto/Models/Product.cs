using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required, MaxLength(250)]
        public string Name { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }

        [Required, Column(TypeName = "money")]
        public decimal Amount { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Condition { get; set; }

 
        [NotMapped]
        public decimal Count { get; set; }

        [Required, Column(TypeName = "ntext")]
        public string About { get; set; }

        [Required, Column(TypeName = "ntext")]
        public string Desc { get; set; }

        public int AdminId { get; set; }

        public Admin Admin { get; set; }


        public List<ProductImages> ProductImages { get; set; }

        public List<ProductToCategory> ProductToCategory { get; set; }

        [NotMapped]
        public HttpPostedFileBase[] ImageFile { get; set; }

        [NotMapped]
        public int[] CategoryId { get; set; }

        public DateTime PostDate { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }

        [Column(TypeName = "bit")]
        public bool isNewlyAdded { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }

        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }
        public List<Sale> Sales { get; set; }

        public List<ReviewProduct> ReviewProducts { get; set; }

        [Column(TypeName = "bigint")]
        public long ViewCount { get; set; }

    }
}