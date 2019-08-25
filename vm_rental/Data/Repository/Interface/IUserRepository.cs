using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using vm_rental.Data.Repository.Interface.Common;
using vm_rental.Data.Model;

namespace vm_rental.Data.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        User CreateUser(Client client);
        Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken);
        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
