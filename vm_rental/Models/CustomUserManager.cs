using Microsoft.AspNetCore.Owin;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using vm_rental.Data.Model;
using vm_rental.ViewModels;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Models
{
  public class CustomUserManager : UserManager<User>
  {
    private readonly IUserRepository userRepository;
    private readonly IClientRepository clientRepository;
    private readonly IUserHistoryRepository userHistoryRepository;
    private readonly IClientHistoryRepository clientHistoryRepository;

    public CustomUserManager(IUserHistoryRepository userHistoryRepository,
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
                             ILogger<UserManager<User>> logger) : 
     
    base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
    {
      this.userRepository = userRepository;
      this.clientRepository = clientRepository;
      this.userHistoryRepository = userHistoryRepository;
      this.clientHistoryRepository = clientHistoryRepository;
    }

    public override Task<IdentityResult> CreateAsync(User user)
    {
      Task<IdentityResult> result = base.CreateAsync(user);
      return result;
    }

    public async Task<IdentityResult> CreateUser(ClientViewModel clientVM)
    {
      Client client = new Client()
      {
        FirmName = clientVM.FirmName,
        FirmOwner = clientVM.OwnerName,
        FirmEmail = clientVM.Email,
        FirmPhone = clientVM.PhoneFull,
        State = clientVM.State,
        City = clientVM.City,
        IsFirm = Convert.ToByte(clientVM.IsBusinessClient),
      };

      User user = new User()
      {
        UserName = clientVM.UserName,
        Email = clientVM.Email,
        PhoneNumber = clientVM.PhoneFull,
        PasswordHash = clientVM.Password,
        FirstName = clientVM.FirstName,
        LastName = clientVM.LastName,
        Client = client
      };

      clientRepository.Add(client);

      IdentityResult result = await base.CreateAsync(user);

      userHistoryRepository.CreateInitialHistory(user, user);

      clientHistoryRepository.CreateInitialHistory(client, user);

      return IdentityResult.Success;
    }
  }
}
