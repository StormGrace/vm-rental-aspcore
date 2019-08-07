using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace vm_rental.ViewModels.Account
{

    public class ClientViewModel 
    {
        public string userName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string firmName { get; set; }     
        public string ownerFirstName { get; set; }
        public string ownerLastName { get; set; }
        public string ownerState { get; set; }
        public string ownerCity { get; set; }
        public string ownerPhone { get; set; }

        public bool _isBusinessClient = false;

        public bool isBusinessClient
        {
            get
            {
                  return _isBusinessClient;
            }
            set
            {
                  _isBusinessClient = value;
          
                  if(firmName == null && (firstName != null && lastName != null))
                  {
                        firmName = firstName + " " + lastName;
                  }  
             }
        }
    }
    public class ClientValidator : AbstractValidator<ClientViewModel>
    {
        Regex latinRegex = new Regex("^[a-zA-Z0-9]+$");

        Regex passwordRegex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*/ d)(?=.*[@$!% *? &])[A-Za-z / d@$!% *? &]{8,}$");
        Regex usernameRegex = new Regex("^[a-zA-Z0-9]+(?:[ _-][A-Za-z0-9]+)*$");
        Regex phoneRegex = new Regex("^[0-9]*$");
        public ClientValidator()
        {
            RuleFor(client => client.userName).NotNull().WithMessage("Please enter your Username.")
                                              .MinimumLength(4).WithMessage("Username is too short. Username can't be shorter than 4 characters")
                                              .MaximumLength(20).WithMessage("Username is too long. Username can't be longer than 20 characters.")
                                              .Matches(usernameRegex).WithMessage("Please enter a valid Username. Only Latin, hyphens and underscores are supported.");

            RuleFor(client => client.email).NotNull().WithMessage("Please enter your Email Address.")
                                           .Matches(latinRegex).WithMessage("Email Address supports only Latin characters.")
                                           .MaximumLength(254).WithMessage("Email Address is too long.")
                                           .EmailAddress().WithMessage("Please enter a valid Email Address.");


            RuleFor(client => client.password).NotNull().WithMessage("Please enter your Password.")
                                              .Matches(latinRegex).WithMessage("Password supports only Latin characters.")
                                              .MinimumLength(8).WithMessage("Password is too short. Password can't be shorter than 8 characters.")
                                              .MaximumLength(100).WithMessage("Password is too long. Password can't exceed 100 characters.")
                                              .Matches(passwordRegex).WithMessage("Password must be at least 8 characters long and contain at least one 0-9, a-z, A-Z and special characters.");
        

            RuleFor(client => client.firmName).NotNull().WithMessage("Please enter your Business's Name.")
                                              .Matches(latinRegex).WithMessage("Business's Name supports only Latin characters.");

            RuleFor(client => client.firstName).NotNull().WithMessage("Please enter your First Name.")
                                               .Matches(latinRegex).WithMessage("First Name supports only Latin characters.");

            RuleFor(client => client.lastName).NotNull().WithMessage("Please enter your Last Name.")
                                              .Matches(latinRegex).WithMessage("Last Name supports only Latin characters.");

            RuleFor(client => client.state).NotNull().WithMessage("Invalid State.")
                                           .Matches(latinRegex).WithMessage("Invalid State.");

            RuleFor(client => client.city).NotNull().WithMessage("Invalid City.")
                                          .Matches(latinRegex).WithMessage("Invalid City.");

            RuleFor(client => client.phone).NotNull().WithMessage("Please enter your Phone.")
                                          .Matches(phoneRegex).WithMessage("Phone Number should contain only numbers.");
        }
    }
}
