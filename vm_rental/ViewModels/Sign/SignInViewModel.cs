using FluentValidation;
using vm_rental.Data.Repository.Interface;
using vm_rental.ViewModels.RuleBuilders;

namespace vm_rental.ViewModels.Sign
{
    public class SignInValidator:AbstractValidator<SignInViewModel>
    {
        private readonly IUserRepository _userRepository;
        public SignInValidator() { }

        public SignInValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(client => client.Password).Cascade(CascadeMode.StopOnFirstFailure).PassWord();
            RuleFor(client => client.EmailOrUsername).Cascade(CascadeMode.StopOnFirstFailure).EmailOrUsername();
        }
    }

    public class SignInViewModel:SignInValidator
    {

        private string emailorusername;
        private string password;
        public SignInViewModel() : base() { }
        public SignInViewModel(IUserRepository userRepository) : base(userRepository) { }



            public string EmailOrUsername
        {
            get { return emailorusername; }
            set { emailorusername = value;
                if (!string.IsNullOrEmpty(emailorusername))
                {
                    emailorusername = emailorusername.Trim();
                }
            }
        }

        public string Password {
            get{
                return password;
            }
            set{
                password = value;
                if (!string.IsNullOrEmpty(password))
                {
                    password = password.Trim();
                }

            }

        }

    }
}
