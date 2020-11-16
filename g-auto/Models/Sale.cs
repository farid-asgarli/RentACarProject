using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace g_auto.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey("Admin")]
        public Nullable<int> CancelledAdminId { get; set; }
        public Nullable<DateTime> CancelledDate { get; set; }
        public int ProductId { get; set; }
        [Required, Column(TypeName = "money")]

        public decimal Amount { get; set; }
        [Required, Column(TypeName = "money")]
        public decimal Price { get; set; }
        public DateTime PostDate { get; set; }
        public User User { get; set; }

        public Admin Admin { get; set; }
        public Product Product { get; set; }

        [MaxLength(500)]
        public string OrderNote { get; set; }

        [Column(TypeName ="bit")]
        public bool IsRefunded { get; set; }
        [Column(TypeName = "bit")]
        public bool isCancelled { get; set; }
        [Column(TypeName = "bit")]
        public bool isPending { get; set; }
        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
        [Column(TypeName = "bit")]
        public bool isFinished { get; set; }

        [Column(TypeName = "bit")]
        public bool isRefundRequested { get; set; }
        public Nullable<DateTime> RefundDate { get; set; }
        public Nullable<DateTime> RefundRequestDate { get; set; }
        public List<Shipment> Shipments { get; set; }
        public List<ReviewProduct> ReviewProducts { get; set; }
    }
}