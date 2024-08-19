using SalesTransactionApp.Models;

namespace SalesTransactionApp.Services.Interface
{
    public interface ISalesTransact
    {
        Task<IEnumerable<SalesTransact>> Get();
        Task<int> Add(SalesTransact salesTransact);
        Task<int> Update(Guid Id,SalesTransact salesTransact);

        Task<int> Delete(Guid Id);

        Task<IEnumerable<SalesTransact>> Search(string itemName, DateTime? salesDate, string paymentType);

    }
}
