using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Orders Qry")]
    public partial class OrdersQry
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        [StringLength(5)]
        public String CustomerId { get; set; }

        public Int32? EmployeeId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public Int32? ShipVia { get; set; }

        [Column(TypeName = "money")]
        public Decimal? Freight { get; set; }

        [StringLength(40)]
        public String ShipName { get; set; }

        [StringLength(60)]
        public String ShipAddress { get; set; }

        [StringLength(15)]
        public String ShipCity { get; set; }

        [StringLength(15)]
        public String ShipRegion { get; set; }

        [StringLength(10)]
        public String ShipPostalCode { get; set; }

        [StringLength(15)]
        public String ShipCountry { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public String CompanyName { get; set; }

        [StringLength(60)]
        public String Address { get; set; }

        [StringLength(15)]
        public String City { get; set; }

        [StringLength(15)]
        public String Region { get; set; }

        [StringLength(10)]
        public String PostalCode { get; set; }

        [StringLength(15)]
        public String Country { get; set; }
    }
}
