using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Order Subtotals")]
    public partial class OrderSubtotal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        [Column(TypeName = "money")]
        public Decimal? Subtotal { get; set; }
    }
}
