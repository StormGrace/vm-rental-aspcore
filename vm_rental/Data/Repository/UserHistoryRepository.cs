using System;
using System.Text;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Data.Repository
{
    public class UserHistoryRepository : Repository<UserHistory>, IUserHistoryRepository
    {
        public UserHistoryRepository(VmDbContext context) : base(context) { }

        public void CreateHistoryFor(User toChange, User by, string changes)
        {
          throw new NotImplementedException();
        }

        public void CreateHistoryForBy(User toChange, int recordId, User by, string changes)
        {
          throw new NotImplementedException();
        }

        public void CreateInitialHistory(User toUser, User byUser)
        {
            if(toUser != null && byUser != null) {
                UserHistory userHistory = new UserHistory()
                {
                    Changes = "Initial",
                    Version = 1,
                    Username = toUser.UserName,
                    Email = toUser.Email,
                    PhoneNumber = toUser.PhoneNumber,
                    PasswordHash = Encoding.ASCII.GetBytes(toUser.PasswordHash),
                    FirstName = toUser.FirstName,
                    LastName = toUser.LastName,
                    IsActive = 1,
                    DateCreated = DateTimeOffset.UtcNow,
                    CreatedByNavigation = byUser,
                };
          
                Add(userHistory);
            }
         }

        public void CreateInitialHistoryForBy(User toChange, int recordId, User createdBy)
        {
          throw new NotImplementedException();
        }
  }
}
