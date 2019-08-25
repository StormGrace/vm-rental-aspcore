using FluentValidation;
using System.Text.RegularExpressions;

namespace vm_rental.ViewModels.RuleBuilders
{
  public static class GlobalRules
  {
    //General Rules
    public static IRuleBuilderOptions<T, string> MustBeGreaterAndLessInclusive<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
    {
      return ruleBuilder.MinimumLength(min).MaximumLength(max);
    }

    public static IRuleBuilderOptions<T, string> IsLatinSpecial<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = "Only Latin Characters are supported")
    {
      return ruleBuilder.Matches(new Regex("^[ -~]+$")).WithMessage(errorMessage);
    }

    public static IRuleBuilderOptions<T, string> IsLatin<T>(this IRuleBuilder<T, string> ruleBuilder, string errorMessage = "Only Latin Characters are supported")
    {
      return ruleBuilder.Matches(new Regex("^[a-zA-Z ]+$")).WithMessage(errorMessage);
    }
  }
}
