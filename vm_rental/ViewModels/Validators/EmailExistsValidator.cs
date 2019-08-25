using FluentValidation.Validators;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.ViewModels.Validators
{
  public class EmailExistsValidator : PropertyValidator
  {
    private readonly IUserRepository _userRepository;

    //DI
    public EmailExistsValidator(IUserRepository userRepository) : base("Email is already taken.")
    {
      _userRepository = userRepository;
    }

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      if (validatorContext.PropertyValue != null)
      {
        string email = (string)validatorContext.PropertyValue;

        if (_userRepository.EmailExists(email))
        {
          return false;
        }
      }

      return true;
    }
  }
}
