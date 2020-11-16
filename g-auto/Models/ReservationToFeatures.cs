using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace g_auto.Models
{
    public class ReservationToFeatures
    {
        public int Id { get; set; }

        [ForeignKey("FeatureSet")]
        public int FeatureSetId { get; set; }

        [ForeignKey("Reservation")]

        public int ReservationId { get; set; }

        public Reservation Reservation { get; set; }

        public FeatureSet FeatureSet { get; set; }
    }
}