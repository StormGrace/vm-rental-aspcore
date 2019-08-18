using FluentValidation;
using System.Text.RegularExpressions;
using vm_rental.Data.Interface;
using vm_rental.ViewModels.Validators;

namespace vm_rental.ViewModels.RuleBuilders
{
  public static class FieldRules
  {
    ////Email Rules
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder, IUserHistoryRepository userHistoryRepository)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Email address.")
                        .MinimumLength(3).WithMessage("Email address is too short.")
                        .MaximumLength(254).WithMessage("Email address is too long.")
                        .EmailAddress().WithMessage("Please enter a valid Email address.")
                        .SetValidator(new EmailExistsValidator(userHistoryRepository));
    }
    ////
    ////Username Rules
    public static IRuleBuilderOptions<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder, IUserHistoryRepository userHistoryRepository)
    {
      int minLength = 4, maxLength = 20;

      return ruleBuilder.NotNull().WithMessage("Please enter your Username.")
                        .MinimumLength(minLength).WithMessage($"Username must be at least {minLength} characters long.")
                        .MaximumLength(maxLength).WithMessage($"Username is too long.")
                        .Matches(new Regex("^[a-zA-Z0-9]+(?:[ _-][A-Za-z0-9]+)*$")).WithMessage("Invalid Username. Only hyphens and underscores are allowed.")
                        .SetValidator(new UsernameReservedValidator())
                        .SetValidator(new UsernameExistsValidator(userHistoryRepository));
    }
    ////
    ////Password Rules
    public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      int minLength = 8, maxLength = 20;

      return ruleBuilder.NotNull().WithMessage("Please enter your Password.")
                        .MinimumLength(minLength).WithMessage($"Password must be at least {minLength} characters long.")
                        .MaximumLength(maxLength).WithMessage($"Password is too long.")
                        .Matches(new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]+$"))
                        .WithMessage("Password is invalid. 1 uppercase and lowercase letter, 1 special character and 1 number is required.");
    }                   
    ////
    ////Firstname Rules
    public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your First name.")
                        .MinimumLength(2).WithMessage("First name is too short.")
                        .MaximumLength(30).WithMessage("First name is too long.")
                        .Matches(new Regex("^[a-zA-Z ,.'-]+$")).WithMessage("Invalid format.");
    }
    ////
    ////Lastname Rules
    public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Last name.")
                        .MinimumLength(2).WithMessage("Last name is too short.")
                        .MaximumLength(30).WithMessage("Last name is too long.")
                        .Matches(new Regex("^[a-zA-Z ,.'-]+$")).WithMessage("Invalid format.");
    }
    ////
    ////State Rules
    public static IRuleBuilderOptions<T, string> State<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your State.")
                        .MinimumLength(4).WithMessage("State name is too short.")
                        .MaximumLength(30).WithMessage("State name is too long.")
                        .IsLatin("Invalid format.")
                        .SetValidator(new CountryIsValid());
    }
    ////
    ////City Rules
    public static IRuleBuilderOptions<T, string> City<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your City.")
                        .MinimumLength(3).WithMessage("City name is too short.")
                        .MaximumLength(30).WithMessage("City name is too long.")
                        .IsLatin("Invalid format.");
    }
    ////
    ////Phone Rules
    public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Phone number.")
                        .Matches(new Regex("^[0-9]+$")).WithMessage("Phone number must contain only numbers.")
                        .MinimumLength(6).WithMessage("Phone number is too short.")
                        .MaximumLength(12).WithMessage("Phone number is too long.");                  
    }
    ////
    ////Firmname Rules
    public static IRuleBuilderOptions<T, string> FirmName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Firm's name.")
                        .MinimumLength(2).WithMessage("Your Firm's Name is too short.")
                        .MaximumLength(30).WithMessage("Your Firm's Name is too long.")
                        .IsLatinSpecial("Invalid format.");
    }
    ////
  }
}
