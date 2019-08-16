using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.ViewModels;

namespace vm_rental.Models
{
  public interface IUserManager
  {
    void RegisterClient(ClientViewModel clientVM);
  }
  public class UserManager : IUserManager
  {
    private readonly IClientRepository _clientRepository;
    private readonly IClientHistoryRepository _clientHistoryRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserHistoryRepository _userHistoryRepository;

    public UserManager(IClientRepository clientRepo, IClientHistoryRepository clientHistoryRepo,
                       IUserRepository userRepo, IUserHistoryRepository userHistoryRepo)
    {
      _clientRepository = clientRepo;
      _clientHistoryRepository = clientHistoryRepo; 
      _userRepository = userRepo;
      _userHistoryRepository = userHistoryRepo;
    }

    public void RegisterClient(ClientViewModel clientVM)
    {
      Client client = _clientRepository.CreateClient();
      User user = _userRepository.CreateUser(client);

      _ = _clientHistoryRepository
             .CreateClientHistory(clientVM.FirmName, clientVM.FirstName, 
                                  clientVM.LastName, clientVM.Email, 
                                  clientVM.Phone, clientVM.State,
                                  clientVM.City, clientVM.IsBusinessClient, client, user);

      _ = _userHistoryRepository
            .CreateUserHistory(clientVM.UserName, clientVM.Email,
                               clientVM.Password, clientVM.FirstName,
                               clientVM.LastName, clientVM.Phone, user);
    }
  }
}
