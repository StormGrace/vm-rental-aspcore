using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Sign;
using vm_rental.Data.Repository.Interface;
using System.Security.Claims;
using vm_rental.Utility.Services.Auth;
using vm_rental.Utility.Services.Auth.JWT;
using vm_rental.Data;

namespace vm_rental.Models.Identity
{
  public class CustomUserManager : UserManager<User>
  {
    private readonly JWTService _jwtService;
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IUserHistoryRepository _userHistoryRepository;
    private readonly IClientHistoryRepository _clientHistoryRepository;

    public CustomUserManager
    (
      IAuthService authService,
      IUserHistoryRepository userHistoryRepository,
      IClientRepository clientRepository,
      IClientHistoryRepository clientHistoryRepository,
      IUserRepository userRepository,
      IUserStore<User> userStore,
      IOptions<IdentityOptions> optionsAccessor,
      IPasswordHasher<User> passwordHasher,
      IEnumerable<IUserValidator<User>> userValidators,
      IEnumerable<IPasswordValidator<User>> passwordValidators,
      ILookupNormalizer keyNormalizer,
      IdentityErrorDescriber errors,
      IServiceProvider services,
      ILogger<UserManager<User>> logger
    )
    : base(userStore, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
      _userRepository = userRepository;
      _clientRepository = clientRepository;
      _userHistoryRepository = userHistoryRepository;
      _clientHistoryRepository = clientHistoryRepository;
      _jwtService = (JWTService)authService;

      base.PasswordHasher = passwordHasher;
    }

    public void ConfirmUserEmail(User user)
    {
      if(user.UserName != null)
      {
        _userRepository.ConfirmEmail(user.UserName);
      }
    }

    public override Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
      string emailToken = null;

      if(user != null)
      {
        Claim[] userClaims = new Claim[]
        {
          new Claim("username", user.UserName),
          new Claim("password", user.PasswordHash)
        };


        emailToken = _jwtService.GenerateJwtToken(userClaims, TimeSpan.FromHours(24));
      }

      return Task.FromResult(emailToken);
    }

    public override Task<IdentityResult> CreateAsync(User user)
    {
      Task<IdentityResult> result = base.CreateAsync(user);
      return result;
    }

    public async Task<User> CreateUser(SignUpViewModel signUpVM)
    {
      DateTime dateCreated = DateTime.UtcNow;

      Client client = new Client()
      {
        FirmName = signUpVM.FirmName,
        FirmOwner = signUpVM.OwnerName,
        FirmEmail = signUpVM.Email,
        FirmPhone = signUpVM.PhoneFull,
        State = signUpVM.State,
        City = signUpVM.City,
        IsFirm = Convert.ToByte(signUpVM.IsBusinessClient),
        DateCreated = dateCreated,
      };

      User user = new User()
      {
        UserName = signUpVM.UserName,
        Email = signUpVM.Email,
        PhoneNumber = signUpVM.PhoneFull,
        PasswordHash = signUpVM.Password,
        FirstName = signUpVM.FirstName,
        LastName = signUpVM.LastName,
        Client = client,
        DateCreated = dateCreated,
      };

      try
      {
        _clientRepository.Add(client);

        await base.CreateAsync(user, user.PasswordHash);

        _userHistoryRepository.CreateInitialHistory(user, user);

        _clientHistoryRepository.CreateInitialHistory(client, user);
      }
      catch(Exception)
      {
        throw new Exception("Error occured while saving data to the database.");
      }

      return user;
    }
  }
}
