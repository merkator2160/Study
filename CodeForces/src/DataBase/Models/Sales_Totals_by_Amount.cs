using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Sales Totals by Amount")]
    public partial class SalesTotalsByAmount
    {
        [Column(TypeName = "money")]
        public Decimal? SaleAmount { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 OrderId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public String CompanyName { get; set; }

        public DateTime? ShippedDate { get; set; }
    }
}
