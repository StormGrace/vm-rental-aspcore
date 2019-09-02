using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using vm_rental.Data.Repository.Interface.Common;
using vm_rental.Data.Model;

namespace vm_rental.Data.Repository.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        void CreateUser(User user);
        User FindByIdAsync(string id);
        User FindByNameAsync(string username);
        User FindByEmailAsync(string email);
        void ConfirmEmail(string username);
        bool UsernameExists(string username);
        bool EmailExists(string email);
    }
}
