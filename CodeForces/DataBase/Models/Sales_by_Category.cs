using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Sales by Category")]
    public partial class SalesByCategory
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 CategoryId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(15)]
        public String CategoryName { get; set; }

        [Key]
        [Column(Order = 2)]
        [StringLength(40)]
        public String ProductName { get; set; }

        [Column(TypeName = "money")]
        public Decimal? ProductSales { get; set; }
    }
}
