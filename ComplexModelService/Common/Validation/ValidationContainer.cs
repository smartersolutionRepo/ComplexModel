namespace ComplexModelService.Common.Validation
{
    using System.Collections.Generic;

    using ComplexModelCore.Validation;

    public partial class ValidationContainer<T> : IValidationContainer<T>
    {
        public IDictionary<string, IList<string>> ValidationErrors { get; private set; }
        public T Entity { get; private set; }

        public bool IsValid
        {
            get { return this.ValidationErrors.Count == 0; }
        }

        public ValidationContainer(IDictionary<string, IList<string>> validationErrors, T entity)
        {
            this.ValidationErrors = validationErrors;
            this.Entity = entity;
        }
    }
}
