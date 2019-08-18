using FluentValidation.Validators;
using vm_rental.Data.Interface;


namespace vm_rental.ViewModels.Validators
{
  public class UsernameExistsValidator : PropertyValidator
  {
    IUserHistoryRepository _userHistoryRepository;

    //DI
    public UsernameExistsValidator(IUserHistoryRepository userHistoryRepository) : base("Username is already taken.")
    {
      _userHistoryRepository = userHistoryRepository;
    }

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      if (validatorContext.PropertyValue != null)
      {
        string username = (string)validatorContext.PropertyValue;

        if (_userHistoryRepository.UsernameExists(username))
        {
          return false;
        }
      }
      return true;
    }
  }
}
