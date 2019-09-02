using FluentValidation;

namespace vm_rental.ViewModels.RuleBuilders.SignInRules
{
    public static class SignInRules
    {   
        public static IRuleBuilderOptions<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage("Please enter your Password.");
        }

        public static IRuleBuilderOptions<T,string> EmailOrUsername<T>(this IRuleBuilder<T,string>ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage("Please enter your Email address or Username.");
        }
           
    }
}
