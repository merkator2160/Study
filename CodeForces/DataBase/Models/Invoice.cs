using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public partial class Invoice
    {
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

        [StringLength(5)]
        public String CustomerId { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public String CustomerName { get; set; }

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

        [Key]
        [Column(Order = 1)]
        [StringLength(31)]
        public String Salesperson { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(40)]
        public String ShipperName { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 ProductId { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(40)]
        public String ProductName { get; set; }

        [Key]
        [Column(Order = 6, TypeName = "money")]
        public Decimal UnitPrice { get; set; }

        [Key]
        [Column(Order = 7)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 Quantity { get; set; }

        [Key]
        [Column(Order = 8)]
        public Single Discount { get; set; }

        [Column(TypeName = "money")]
        public Decimal? ExtendedPrice { get; set; }

        [Column(TypeName = "money")]
        public Decimal? Freight { get; set; }
    }
}
