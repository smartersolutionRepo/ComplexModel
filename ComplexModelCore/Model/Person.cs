using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ComplexModelCore.Model
{
    public class Person:DomainObject
    {
        [Key]
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
       
    }



    public class PersonVM
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Occupation { get; set; }
        public string Company { get; set; }
    }
}