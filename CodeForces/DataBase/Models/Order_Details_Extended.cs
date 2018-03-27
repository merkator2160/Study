using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Order Details Extended")]
    public partial class OrderDetailsExtended
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 ProductId { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public String ProductName { get; set; }

        [Key]
        [Column(Order = 3, TypeName = "money")]
        public Decimal UnitPrice { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int16 Quantity { get; set; }

        [Key]
        [Column(Order = 5)]
        public Single Discount { get; set; }

        [Column(TypeName = "money")]
        public Decimal? ExtendedPrice { get; set; }
    }
}
