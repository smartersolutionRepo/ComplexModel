using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ComplexModelCore.Model
{
    public class student:DomainObject
    {
        public student()
        {
            products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string studentname { get; set; }
        public string studentclass { get; set; }
        public int Age { get; set; }

        public int customerID { get; set; }
        [ForeignKey("customerID")]
        public virtual Customers customers { get; set; }
        public virtual ICollection<Product> products { get; set; }
    }

    public class studentviewmodel
    {
        [Key]
        public int Id { get; set; }
        public string studentname { get; set; }
        public int customerID { get; set; }
        public string studentclass { get; set; }
        public int Age { get; set; }
        public int[] products { get; set; }
    }
}