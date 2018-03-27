using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    [Table("Alphabetical list of products")]
    public partial class AlphabeticalListOfProduct
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Int32 ProductId { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(40)]
        public String ProductName { get; set; }

        public Int32? SupplierId { get; set; }

        public Int32? CategoryId { get; set; }

        [StringLength(20)]
        public String QuantityPerUnit { get; set; }

        [Column(TypeName = "money")]
        public Decimal? UnitPrice { get; set; }

        public Int16? UnitsInStock { get; set; }

        public Int16? UnitsOnOrder { get; set; }

        public Int16? ReorderLevel { get; set; }

        [Key]
        [Column(Order = 2)]
        public Boolean Discontinued { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(15)]
        public String CategoryName { get; set; }
    }
}
