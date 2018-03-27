using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Order Details")]
    public partial class OrderDetail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 ProductId { get; set; }

        [Column(TypeName = "money")]
        public Decimal UnitPrice { get; set; }

        public Int16 Quantity { get; set; }

        public Single Discount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
