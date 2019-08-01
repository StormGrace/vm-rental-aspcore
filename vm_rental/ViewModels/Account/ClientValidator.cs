using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;

namespace vm_rental.ViewModels.Account
{
    public class ClientValidator : AbstractValidator<ClientViewModel>
    {
        public ClientValidator(){
            RuleFor(client => client.email).NotNull().EmailAddress().WithMessage("Please specify a valid email address");
            
        }
    }
}
