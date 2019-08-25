using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using vm_rental.Data.Model;

namespace vm_rental.Data
{
  public partial class VmDbContext : IdentityDbContext<User, UserRole, int, UserClaim, UsersRoles, UserLogin, UserRoleClaim, UserToken>
  {
    public VmDbContext(DbContextOptions<VmDbContext> options) : base(options) { }
  }
}
