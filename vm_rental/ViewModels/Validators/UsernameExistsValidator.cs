using FluentValidation.Validators;
using vm_rental.Data.Repository.Interface;


namespace vm_rental.ViewModels.Validators
{
  public class UsernameExistsValidator : PropertyValidator
  {
    private readonly IUserRepository _userRepository;

    //DI
    public UsernameExistsValidator(IUserRepository userRepository) : base("Username is already taken.")
    {
      _userRepository = userRepository;
    }

    protected override bool IsValid(PropertyValidatorContext validatorContext)
    {
      if (validatorContext.PropertyValue != null)
      {
        string username = (string)validatorContext.PropertyValue;

        if (_userRepository.UsernameExists(username))
        {
          return false;
        }
      }
      return true;
    }
  }
}
