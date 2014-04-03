namespace ComplexModelCore.Model

{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Xml.Serialization;

    public abstract class DomainObject : IValidatableObject
    {
      

        public DateTime? Created { get; set; }

		//public byte[] RowVersion { get; set; }

        public DateTime? Updated { get; set; }

        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Override this method to implement custom validation in your entities
            
            // This is only for making it compile... and returning null will give an exception.
            if (false)
                yield return new ValidationResult("Well, this should not happend...");
        }
    }
}