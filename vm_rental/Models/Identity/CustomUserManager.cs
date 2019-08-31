using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using vm_rental.Data.Model;
using vm_rental.ViewModels.Sign;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Models.Identity
{
  public class CustomUserManager : UserManager<User>
  {
    private readonly IUserRepository userRepository;
    private readonly IClientRepository clientRepository;
    private readonly IUserHistoryRepository userHistoryRepository;
    private readonly IClientHistoryRepository clientHistoryRepository;

    public CustomUserManager
    (
      IUserHistoryRepository userHistoryRepository,
      IClientRepository clientRepository,
      IClientHistoryRepository clientHistoryRepository,
      IUserRepository userRepository,
      IUserStore<User> store,
      IOptions<IdentityOptions> optionsAccessor,
      IPasswordHasher<User> passwordHasher,
      IEnumerable<IUserValidator<User>> userValidators,
      IEnumerable<IPasswordValidator<User>> passwordValidators,
      ILookupNormalizer keyNormalizer,
      IdentityErrorDescriber errors,
      IServiceProvider services,
      ILogger<UserManager<User>> logger
    )
    : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
      this.userRepository = userRepository;
      this.clientRepository = clientRepository;
      this.userHistoryRepository = userHistoryRepository;
      this.clientHistoryRepository = clientHistoryRepository;
      base.PasswordHasher = passwordHasher;
    }

    public override Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
      return base.GenerateEmailConfirmationTokenAsync(user);
    }

    public override Task<IdentityResult> CreateAsync(User user)
    {
      Task<IdentityResult> result = base.CreateAsync(user);
      return result;
    }

    public async Task<User> CreateUser(SignUpViewModel signUpVM)
    {

      Client client = new Client()
      {
        FirmName = signUpVM.FirmName,
        FirmOwner = signUpVM.OwnerName,
        FirmEmail = signUpVM.Email,
        FirmPhone = signUpVM.PhoneFull,
        State = signUpVM.State,
        City = signUpVM.City,
        IsFirm = Convert.ToByte(signUpVM.IsBusinessClient),
        DateCreated = DateTime.UtcNow,
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
        DateCreated = DateTime.UtcNow,
      };

      clientRepository.Add(client);

      await base.CreateAsync(user, user.PasswordHash);

      userHistoryRepository.CreateInitialHistory(user, user);

      clientHistoryRepository.CreateInitialHistory(client, user);

      return user;
    }
  }
}
