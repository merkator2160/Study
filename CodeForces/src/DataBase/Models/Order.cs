using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public Int32 OrderId { get; set; }

        [StringLength(5)]
        public String CustomerId { get; set; }

        public Int32? EmployeeId { get; set; }

        public DateTime? OrderDate { get; set; }

        public DateTime? RequiredDate { get; set; }

        public DateTime? ShippedDate { get; set; }

        public Int32? ShipVia { get; set; }

        [Column(TypeName = "money")]
        public Decimal? Freight { get; set; }

        [StringLength(40)]
        public String ShipName { get; set; }

        [StringLength(60)]
        public String ShipAddress { get; set; }

        [StringLength(15)]
        public String ShipCity { get; set; }

        [StringLength(15)]
        public String ShipRegion { get; set; }

        [StringLength(10)]
        public String ShipPostalCode { get; set; }

        [StringLength(15)]
        public String ShipCountry { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }

        public virtual Shipper Shipper { get; set; }
    }
}
