using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Customer and Suppliers by City")]
    public partial class CustomerAndSuppliersByCity
    {
        [StringLength(15)]
        public String City { get; set; }

        [Key]
        [Column(Order = 0)]
        [StringLength(40)]
        public String CompanyName { get; set; }

        [StringLength(30)]
        public String ContactName { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(9)]
        public String Relationship { get; set; }
    }
}
