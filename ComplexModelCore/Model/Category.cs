using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ComplexModelCore.Model
{
    public class Category:DomainObject
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CatDesc { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }

    public class CategoryViewModel
    {    
        [Key]
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string CatDesc { get; set; }
        public int[] Products { get; set; }
    }

   


}