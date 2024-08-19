using SalesTransactionApp.Models;

namespace SalesTransactionApp.Services.Interface
{
    public interface IUserService
    {
        Task<UserProfile> GetUserByLoginIdAsync(string loginId);
        Task<bool> ValidateUserCredentialsAsync(string loginId, string password);
    }
}
