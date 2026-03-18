using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VolleyballManager.Tests.Helpers
{
    public static class ModelValidator
    {
        public static List<ValidationResult> Validate(object model)
        {
            var validationResults = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, validationResults, true);
            return validationResults;
        }
    }
}