using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplexModelCore.Model
{
    public class Product:DomainObject
    {
        [Key]
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
        public virtual ICollection<student> Students { get; set; }
    }
}