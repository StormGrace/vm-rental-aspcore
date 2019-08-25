using FluentValidation.Validators;
using vm_rental.Data.JSON;

namespace vm_rental.ViewModels.Validators
{
  public class CountryIsValid : PropertyValidator
  {
    public CountryIsValid() : base("No such state."){}

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      if (validatorContext.PropertyValue != null)
      {
        string state = (string)validatorContext.PropertyValue;

        if (JSONRepository.countries.CountryExists(state))
        {
          return true;
        }
      }
      return false;
    }
  }
}
