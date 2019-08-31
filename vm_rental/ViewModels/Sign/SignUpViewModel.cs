using System.Linq;
using FluentValidation;
using vm_rental.Data.JSON;
using vm_rental.Data.Repository.Interface;
using vm_rental.ViewModels.RuleBuilders;

namespace vm_rental.ViewModels.Sign
{
  //#Globata
  public class SignUpValidator : AbstractValidator<SignUpViewModel>
  {
    private readonly IUserRepository _userRepository;
    public SignUpValidator(){}
    public SignUpValidator(IUserRepository userRepository)
    {
      _userRepository = userRepository;

      RuleFor(client => client.UserName).Cascade(CascadeMode.StopOnFirstFailure).Username(_userRepository);
      RuleFor(client => client.Email).Cascade(CascadeMode.StopOnFirstFailure).Email(_userRepository);
      RuleFor(client => client.Password).Cascade(CascadeMode.StopOnFirstFailure).Password();
      RuleFor(client => client.FirstName).Cascade(CascadeMode.StopOnFirstFailure).FirstName();
      RuleFor(client => client.LastName).Cascade(CascadeMode.StopOnFirstFailure).LastName();
      RuleFor(client => client.State).Cascade(CascadeMode.StopOnFirstFailure).State();
      RuleFor(client => client.City).Cascade(CascadeMode.StopOnFirstFailure).City();
      RuleFor(client => client.Phone).Cascade(CascadeMode.StopOnFirstFailure).Phone();
      RuleFor(client => client.FirmName).Cascade(CascadeMode.StopOnFirstFailure).FirmName();
    }
  }

  public class SignUpViewModel : SignUpValidator
  {
    private string userName;
    private string email;
    private string password;
    private string firstName;
    private string lastName;
    private string state;
    private string city;
    private string phone;
    private string firmName;

    public SignUpViewModel() : base(){}
    public SignUpViewModel(IUserRepository userRepository) : base (userRepository){}

    public string UserName {
      get {
        return userName;
      }
      set {
        userName = value;

        if (!string.IsNullOrEmpty(userName)) {
          userName = userName.Trim();
        }
      }
    }

    public string Email {
      get {
        return email;
      }
      set {
        email = value;

        if (!string.IsNullOrEmpty(email))
        {
          email = email.Trim();
        }
      }
    }

    public string Password {
      get {
        return password;
      }
      set {
        password = value;

        if (!string.IsNullOrEmpty(password))
        {
          password = password.Trim();
        }
      }
    }

    public string FirstName {
      get {
        return firstName;
      }
      set {
        firstName = GetCaseNormalizedString(value);
      }
    }

    public string LastName {
      get {
        return lastName;
      }
      set {
        lastName = GetCaseNormalizedString(value);
      }
    }

    public string OwnerName
    {
      get
      {
        return FirstName + " " + LastName;
      }
    }

    public string State {
      get {
        return state;
      }
      set {
        state = GetCaseNormalizedString(value);
      }
    }

    public string City {
      get {
        return city;
      }
      set {
        city = GetCaseNormalizedString(value);
      }
    }

    public string Phone {
      get {
        return phone;
      }
      set {
        phone = value;

        if (!string.IsNullOrEmpty(phone))
        {
          phone = phone.Trim();
        }
      }
    }

    public string PhoneFull
    {
      get
      {
        return $"[{JSONRepository.countries.GetCountryCodeByCountryName(State)}] " + Phone;
      }
    }

    public string FirmName {
      get {
        return firmName;
      }
      set {
        firmName = GetCaseNormalizedString(value);
      }
    }

    public bool _isBusinessClient = false;

    public bool IsBusinessClient
    {
      get
      {
        return _isBusinessClient;
      }
      set
      {
        _isBusinessClient = value;

        if (!_isBusinessClient && string.IsNullOrEmpty(FirmName)) //To Be Removed
        {
          FirmName = "N/A";
        }
      }
    }

    public string GetCaseNormalizedString(string str)
    {
      if (!string.IsNullOrEmpty(str))
      {
        str = str.Trim();
        str = string.Concat(char.ToUpper(str.First()), str.Substring(1).ToLower());
      }

      return str;
    }
  }
}
