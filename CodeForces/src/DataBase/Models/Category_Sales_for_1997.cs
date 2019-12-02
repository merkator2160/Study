using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Category Sales for 1997")]
    public partial class CategorySalesFor1997
    {
        [Key]
        [StringLength(15)]
        public String CategoryName { get; set; }

        [Column(TypeName = "money")]
        public Decimal? CategorySales { get; set; }
    }
}
