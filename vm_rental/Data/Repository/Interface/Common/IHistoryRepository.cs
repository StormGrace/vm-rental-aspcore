using vm_rental.Data.Model;

namespace vm_rental.Data.Repository.Interface.Common
{
    public interface IHistoryRepository<T> where T : class
    {
      void CreateInitialHistory(T entityToChange, User createdBy);
      void CreateHistoryFor(T entityToChange, User createdBy, string changes);
      void CreateInitialHistoryForBy(T entityToChange, int recordId, User createdBy);
      void CreateHistoryForBy(T entityToChange, int recordId, User createdBy, string changes);
    }
}
