using System.Linq;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using vm_rental.Data.Interface;
using vm_rental.ViewModels.ValidatorRules;

namespace vm_rental.ViewModels
{
  public class ClientValidator : AbstractValidator<ClientViewModel>
  {
    IUserHistoryRepository _userHistoryRepository;
    public ClientValidator()
    {

    }
    public ClientValidator(IUserHistoryRepository userHistoryRepository)
    {
      _userHistoryRepository = userHistoryRepository;

      RuleFor(client => client.UserName).Cascade(CascadeMode.StopOnFirstFailure).Username();
      RuleFor(client => client.Email).Cascade(CascadeMode.StopOnFirstFailure).Email(_userHistoryRepository);
      RuleFor(client => client.Password).Cascade(CascadeMode.StopOnFirstFailure).Password();
      RuleFor(client => client.FirstName).FirstName();
      RuleFor(client => client.LastName).LastName();
      RuleFor(client => client.State).State();
      RuleFor(client => client.City).City();
      RuleFor(client => client.Phone).Phone();
      RuleFor(client => client.FirmName).FirmName();
    }
  }


  public class ClientViewModel : ClientValidator
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

    public ClientViewModel() : base()
    {

    }
    public ClientViewModel(IUserHistoryRepository userHistoryRepository) : base (userHistoryRepository)
    {

    }

    public string UserName {
      get {
        return userName;
      }
      set {
        userName = value.Trim();
      }
    }

    public string Email {
      get {
        return email;
      }
      set {
        email = value.Trim();
      }
    }

    public string Password {
      get {
        return password;
      }
      set {
        password = value.Trim();
      }
    }

    public string FirstName {
      get {
        return firstName;
      }
      set {
        firstName = GetCaseNormalizedString(value.Trim());
      }
    }

    public string LastName {
      get {
        return lastName;
      }
      set {
        lastName = GetCaseNormalizedString(value.Trim());
      }
    }

    //[Remote("CountryIsValid", "Sign", ErrorMessage = "Invalid State.")]
    public string State {
      get {
        return state;
      }
      set {
        state = GetCaseNormalizedString(value.Trim());
      }
    }

    public string City {
      get {
        return city;
      }
      set {
        city = GetCaseNormalizedString(value.Trim());
      }
    }

    public string Phone {
      get {
        return phone;
      }
      set {
        phone = value.Trim();
      }
    }

    public string FirmName {
      get {
        return firmName;
      }
      set {
        firmName = GetCaseNormalizedString(value.Trim());
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

        if (!_isBusinessClient && FirmName == null && (FirstName != null && LastName != null))
        {
          FirmName = FirstName + " " + LastName;
        }
      }
    }

    public string GetCaseNormalizedString(string input)
    {
      return string.Concat(char.ToUpper(input.First()), input.Substring(1).ToLower());
    }
  }
}
