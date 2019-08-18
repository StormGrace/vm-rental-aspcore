using FluentValidation.Validators;
using vm_rental.Data.JSON;

namespace vm_rental.ViewModels.Validators
{
  public class UsernameReservedValidator : PropertyValidator
  {
    public UsernameReservedValidator() : base("Username is forbidden."){}

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      if (validatorContext.PropertyValue != null)
      {
        string username = (string)validatorContext.PropertyValue;

        if (JSONRepository.reservedWords.WordReserved(username))
        {
          return false;
        }
      }
      return true;
    }
  }
}
