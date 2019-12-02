using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public partial class Supplier
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Supplier()
        {
            Products = new HashSet<Product>();
        }

        public Int32 SupplierId { get; set; }

        [Required]
        [StringLength(40)]
        public String CompanyName { get; set; }

        [StringLength(30)]
        public String ContactName { get; set; }

        [StringLength(30)]
        public String ContactTitle { get; set; }

        [StringLength(60)]
        public String Address { get; set; }

        [StringLength(15)]
        public String City { get; set; }

        [StringLength(15)]
        public String Region { get; set; }

        [StringLength(10)]
        public String PostalCode { get; set; }

        [StringLength(15)]
        public String Country { get; set; }

        [StringLength(24)]
        public String Phone { get; set; }

        [StringLength(24)]
        public String Fax { get; set; }

        [Column(TypeName = "ntext")]
        public String HomePage { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product> Products { get; set; }
    }
}
