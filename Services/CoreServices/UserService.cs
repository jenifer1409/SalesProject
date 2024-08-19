using Dapper;
using SalesTransactionApp.Models;
using SalesTransactionApp.Services.Interface;
using System.Data;
using System.Data.Common;

namespace SalesTransactionApp.Services.CoreServices
{
    public class UserService : IUserService
    {
        private readonly IDbConnection _db;

        public UserService(IDbConnection db)
        {
            _db = db;
        }

        public async Task<UserProfile> GetUserByLoginIdAsync(string loginId)
        {
            var query = "SELECT * FROM Users WHERE LoginID = @LoginID";
            return await _db.QueryFirstOrDefaultAsync<UserProfile>(query, new { LoginID = loginId });
        }

       public async Task<bool> ValidateUserCredentialsAsync(string loginId, string password)
        {
            var user = await GetUserByLoginIdAsync(loginId);

            if (user == null)
                return false;




            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }
    }
}
