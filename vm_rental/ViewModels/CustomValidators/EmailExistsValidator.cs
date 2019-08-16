using FluentValidation.Validators;
using vm_rental.Data.Interface;


namespace vm_rental.ViewModels.CustomValidators
{
  public class EmailExistsValidator : PropertyValidator
  {
    //Server-Side Email Validator
    IUserHistoryRepository _userHistoryRepository;

    //DI
    public EmailExistsValidator(IUserHistoryRepository userHistoryRepository) : base("Email is already taken.")
    {
      _userHistoryRepository = userHistoryRepository;
    }

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      //Bad Practice, but it's a solution to the long chaining of DI.
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
