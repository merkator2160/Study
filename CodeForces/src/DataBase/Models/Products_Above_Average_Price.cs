using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Products Above Average Price")]
    public partial class ProductsAboveAveragePrice
    {
        [Key]
        [StringLength(40)]
        public String ProductName { get; set; }

        [Column(TypeName = "money")]
        public Decimal? UnitPrice { get; set; }
    }
}
