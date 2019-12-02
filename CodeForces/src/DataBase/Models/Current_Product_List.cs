using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Current Product List")]
    public partial class CurrentProductList
    {
        [Key]
        [Column(Order = 0)]
        public Int32 ProductId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public String ProductName { get; set; }
    }
}
