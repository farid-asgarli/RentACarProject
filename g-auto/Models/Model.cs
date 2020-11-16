using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace g_auto.Models
{
    public class Model
    {
        public int Id { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Color { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Condition { get; set; }


        [Required(ErrorMessage = "This field cannot be empty.")]
        public int Doors { get; set; }

        [Required(ErrorMessage = "This field cannot be empty.")]
        public string DriveTrain { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public decimal Engine { get; set; }
        public string EngineLayout { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string FuelType { get; set; }

        [Required(ErrorMessage = "This field cannot be empty.")]
        public int HorsePower { get; set; }

        [DataType(DataType.Currency)]
        [Required(ErrorMessage = "Mass field cannot be empty.")]
        [Column(TypeName = "money")]
        public decimal Mass { get; set; }

        [Column(TypeName = "bigint")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public long Mileage { get; set; }

        [MaxLength(150)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Name { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public decimal PriceDaily { get; set; }


        [Required(ErrorMessage = "This field cannot be empty.")]
        public int Seats { get; set; }

        [MaxLength(250)]
        [Required(ErrorMessage = "This field cannot be empty.")]
        public string Transmission { get; set; }

        [Required(ErrorMessage = "This field cannot be empty.")]
        public int Year { get; set; }


        [Required]
        [Column(TypeName ="bit")]
        public bool hasABS { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool hasAlloyWheels { get; set; }


        [Required]
        [Column(TypeName = "bit")]
        public bool hasESP { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool hasPSensors { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool hasConditioner { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool hasCC { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool hasLeatherInterior { get; set; }

        [Required]
        [Column(TypeName = "bit")]
        public bool hasXenon { get; set; }

        [Required, Column(TypeName = "ntext")]
        public string Description { get; set; }


        public Brand Brand { get; set; }
        public int BrandId { get; set; }

        public int AdminId { get; set; }
        public Admin Admin { get; set; }

        public DateTime PostDate { get; set; }

        [NotMapped]
        public HttpPostedFileBase[] ImageFile { get; set; }

        public List<ModelImages> ModelImages { get; set; }

        [Column(TypeName = "bit")]
        public bool isActive { get; set; }
        public Nullable<DateTime> ModifiedDate { get; set; }


        [ForeignKey("AdminModified")]

        public Nullable<int> AdminModifiedId { get; set; }
        public Admin AdminModified { get; set; }

        public List<Reservation> Reservations { get; set; }
        public List<Review> Reviews { get; set; }

        [Column(TypeName = "bigint")]
        public long ViewCount { get; set; }

    }
}