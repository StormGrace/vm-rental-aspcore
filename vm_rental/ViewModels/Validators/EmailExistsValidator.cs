using FluentValidation.Validators;
using vm_rental.Data.Interface;

namespace vm_rental.ViewModels.Validators
{
  public class EmailExistsValidator : PropertyValidator
  {
    IUserHistoryRepository _userHistoryRepository;

    //DI
    public EmailExistsValidator(IUserHistoryRepository userHistoryRepository) : base("Email is already taken.")
    {
      _userHistoryRepository = userHistoryRepository;
    }

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      if (validatorContext.PropertyValue != null)
      {
        string email = (string)validatorContext.PropertyValue;

        if (_userHistoryRepository.EmailExists(email))
        {
          return false;
        }
      }
        return true;
    }
  }
}
