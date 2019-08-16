using FluentValidation;
using System.Text.RegularExpressions;

namespace vm_rental.ViewModels.ValidatorRules
{
  public static class GlobalRules
  {
    //General Rules
    public static IRuleBuilderOptions<T, string> MustBeGreaterAndLessInclusive<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
    {
      return ruleBuilder.MinimumLength(min).MaximumLength(max);
    }

    public static IRuleBuilderOptions<T, string> IsLatin<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
      return ruleBuilder.Matches(new Regex("^[ -~]+$")).WithMessage("Only latin characters are allowed.");
    }
  }
}
