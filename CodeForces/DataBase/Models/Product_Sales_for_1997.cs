using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Product Sales for 1997")]
    public partial class ProductSalesFor1997
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public String CategoryName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public String ProductName { get; set; }

        [Column(TypeName = "money")]
        public Decimal? ProductSales { get; set; }
    }
}
