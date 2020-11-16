using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public Nullable<DateTime> ESTDelivery { get; set; }
        public Nullable<DateTime> CreatedDate { get; set; }
        [MaxLength(500)]

        public string Address { get; set; }
        [Column(TypeName ="bit")]
        public bool IsReady { get; set; }
        [Column(TypeName = "bit")]

        public bool IsDelivered { get; set; }
        [Column(TypeName = "bit")]

        public bool IsCanceled { get; set; }

        public Sale Sale { get; set; }
        public int SaleId { get; set; }
    }
}