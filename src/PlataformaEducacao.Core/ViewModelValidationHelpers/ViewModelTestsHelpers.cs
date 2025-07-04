using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Core.ViewModelValidationHelpers
{
    public static class ViewModelTestsHelpers
    {
        public static List<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(model);

            Validator.TryValidateObject(model, validationContext, validationResults, validateAllProperties: true);

            return validationResults;
        }
    }
}