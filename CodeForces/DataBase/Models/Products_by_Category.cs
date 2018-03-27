using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Products by Category")]
    public partial class ProductsByCategory
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(15)]
        public String CategoryName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public String ProductName { get; set; }

        [StringLength(20)]
        public String QuantityPerUnit { get; set; }

        public Int16? UnitsInStock { get; set; }

        [Key]
        [Column(Order = 2)]
        public Boolean Discontinued { get; set; }
    }
}
