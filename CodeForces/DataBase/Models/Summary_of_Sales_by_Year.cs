using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Summary of Sales by Year")]
    public partial class SummaryOfSalesByYear
    {
        public DateTime? ShippedDate { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        [Column(TypeName = "money")]
        public Decimal? Subtotal { get; set; }
    }
}