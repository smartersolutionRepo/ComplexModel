using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplexModelCore.Model
{
    public class Customers:DomainObject
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public virtual ICollection<student> students { get; set; }
    }

    public class customerviewmodel
    {
        [Key]
        public int Id { get; set; }
        public string Name{ get; set; }
        public int Age{ get; set; }
        public int[] students { get; set; }
    }
}