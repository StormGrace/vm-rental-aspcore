using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace vm_rental.ViewModels.RuleBuilders
{
    public static class LoginFieldRules
    {
       
        public static IRuleBuilderOptions<T, string> PassWord<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage("Please enter your Password.");
        }
        public static IRuleBuilderOptions<T,string> EmailOrUsername<T>(this IRuleBuilder<T,string>ruleBuilder)
        {
            return ruleBuilder.NotNull().WithMessage("Please enter your Email address or Username.");
        }
           
    }
}
