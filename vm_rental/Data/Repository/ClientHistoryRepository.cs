using System;
using vm_rental.Data.Model;
using vm_rental.Data.Repository.Interface;

namespace vm_rental.Data.Repository
{
    public class ClientHistoryRepository : Repository<ClientHistory>, IClientHistoryRepository
    {
        public ClientHistoryRepository(VmDbContext context) : base(context)
        {

        }

        public void CreateHistoryFor(Client toChange, User createdBy, string changes)
        {
          throw new NotImplementedException();
        }

        public void CreateHistoryForBy(Client toChange, int recordId, User createdBy, string changes)
        {
          throw new NotImplementedException();
        }

        public void CreateInitialHistory(Client toClient, User byUser)
        {
            if(toClient != null && byUser != null) 
            {
                ClientHistory clientHistory = new ClientHistory()
                {
                  Changes = "Initial",
                  FirmName = toClient.FirmName,
                  FirmOwner = toClient.FirmOwner,
                  FirmEmail = toClient.FirmEmail,
                  FirmPhone = toClient.FirmPhone,
                  FirmRegNumber = toClient.FirmRegNumber,
                  VatNumber = toClient.VatNumber,
                  FirmFax = toClient.FirmFax,
                  State = toClient.State,
                  City = toClient.City,
                  Address = toClient.Address,
                  IsFirm = Convert.ToByte(toClient.IsFirm),
                  IsVatTaxed = toClient.IsVatTaxed,
                  IsActive = toClient.IsActive,
                  DateCreated = DateTime.UtcNow,
                  Client = toClient,
                  CreatedByNavigation = byUser,
                };

                Add(clientHistory);
            }
        }

        public void CreateInitialHistoryForBy(Client entityToChange, int recordId, User createdBy)
        {
          throw new NotImplementedException();
        }
    }
}
