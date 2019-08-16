using FluentValidation;
using System.Text.RegularExpressions;
using vm_rental.Data.Interface;
using vm_rental.ViewModels.CustomValidators;

namespace vm_rental.ViewModels.ValidatorRules
{

  public static class FieldRules
  {
    ////Email Rules
    public static IRuleBuilderOptions<T, string> Email<T>(this IRuleBuilder<T, string> ruleBuilder, IUserHistoryRepository userHistoryRepository)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Email address.")
                        .MaximumLength(254).WithMessage("Email address is too long.")
                        .EmailAddress().WithMessage("Please enter a valid Email address.")
                        .SetValidator(new EmailExistsValidator(userHistoryRepository));
    }
    ////
    ////Username Rules
    public static IRuleBuilderOptions<T, string> Username<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      int minLength = 4, maxLength = 20;

      return ruleBuilder.NotNull().WithMessage("Please enter your Username.")
                        .MinimumLength(minLength).WithMessage($"Username must be at least {minLength} characters long.")
                        .MaximumLength(maxLength).WithMessage($"Username is too long.")
                        .Matches(new Regex("^[a-zA-Z0-9]+(?:[ _-][A-Za-z0-9]+)*$")).WithMessage("Invalid Username. Only hyphens and underscores are allowed.");
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
                        .WithMessage("Password is invalid. 1 uppercase and lowercase letter, 1 special character and 1 number is required");
    }                   
    ////
    ////Firstname Rules
    public static IRuleBuilderOptions<T, string> FirstName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your First name.").IsLatin();
    }
    ////
    ////Lastname Rules
    public static IRuleBuilderOptions<T, string> LastName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Last name.").IsLatin();
    }
    ////
    ////State Rules
    public static IRuleBuilderOptions<T, string> State<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your State.").IsLatin();
    }
    ////
    ////City Rules
    public static IRuleBuilderOptions<T, string> City<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your City.").IsLatin();
    }
    ////
    ////Phone Rules
    public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Phone number.")
                        .Matches(new Regex("^[0-9]+$")).WithMessage("Phone number must contain only numbers.");
    }
    ////
    ////Firmname Rules
    public static IRuleBuilderOptions<T, string> FirmName<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.NotNull().WithMessage("Please enter your Firm's name.").IsLatin();
    }
    ////
  }
}
