using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataBase.Models
{
    public partial class Customer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Customer()
        {
            Orders = new HashSet<Order>();
            CustomerDemographics = new HashSet<CustomerDemographic>();
        }

        [StringLength(5)]
        public String CustomerId { get; set; }

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

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CustomerDemographic> CustomerDemographics { get; set; }
    }
}
