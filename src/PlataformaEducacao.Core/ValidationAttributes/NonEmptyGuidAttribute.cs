using System.ComponentModel.DataAnnotations;

namespace PlataformaEducacao.Core.ValidationAttributes
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is Guid guid)
            {
                return guid != Guid.Empty;
            }

            return true;
        }
    }

}
