using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace ContactManagerApi.Validations
{
    public class MustHaveOneElementAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is ICollection collection)
            {
                return collection.Count > 0;
            }

            return false;
        }
    }
}
