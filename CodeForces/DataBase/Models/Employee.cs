using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataBase.Models
{
    public partial class Employee
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Employee()
        {
            Employees1 = new HashSet<Employee>();
            Orders = new HashSet<Order>();
            Territories = new HashSet<Territory>();
        }

        public Int32 EmployeeId { get; set; }

        [Required]
        [StringLength(20)]
        public String LastName { get; set; }

        [Required]
        [StringLength(10)]
        public String FirstName { get; set; }

        [StringLength(30)]
        public String Title { get; set; }

        [StringLength(25)]
        public String TitleOfCourtesy { get; set; }

        public DateTime? BirthDate { get; set; }

        public DateTime? HireDate { get; set; }

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
        public String HomePhone { get; set; }

        [StringLength(4)]
        public String Extension { get; set; }

        [Column(TypeName = "image")]
        public Byte[] Photo { get; set; }

        [Column(TypeName = "ntext")]
        public String Notes { get; set; }

        public Int32? ReportsTo { get; set; }

        [StringLength(255)]
        public String PhotoPath { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees1 { get; set; }

        public virtual Employee Employee1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Territory> Territories { get; set; }
    }
}
