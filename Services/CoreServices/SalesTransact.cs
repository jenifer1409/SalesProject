using Dapper;
using Microsoft.Data.SqlClient;
using SalesTransactionApp.Services.Interface;
using System.Data;
using System.Transactions;

namespace SalesTransactionApp.Services.CoreServices
{

    public class SalesTransact : ISalesTransact
    {
        private readonly IDbConnection _db;

        public SalesTransact(IDbConnection db)
        {
            _db = db;
        }
        public async Task<IEnumerable<Models.SalesTransact>> Get()
        {
            var sql = "SELECT * FROM SalesTransaction WHERE IsDeleted = 0";
            return await _db.QueryAsync<Models.SalesTransact>(sql);
        }

        public async Task<int> Update(Guid Id, Models.SalesTransact salesTransact)
        {
            var sql = "UPDATE SalesTransaction SET Item = @SalesItem, SalesDate = @SalesDate, Amount = @Amount, PaymentType = @PaymentType, UpdatedDate = @UpdatedDate, UpdatedBy = @UpdatedBy WHERE Id = @Id";
            return await _db.ExecuteAsync(sql, new
            {
                salesTransact.Item,
                salesTransact.SalesDate,
                salesTransact.Amount,
                salesTransact.PaymentType,
                UpdatedDate = DateTime.Now,
                salesTransact.UpdatedBy,
                Id
            });
        }

        public async Task<int> Add(Models.SalesTransact salesTransact)
        {
            var sql = "INSERT INTO SalesTransaction (Item, SalesDate, Amount, PaymentType, CreatedBy) VALUES (@SalesItem, @SalesDate, @Amount, @PaymentType, @CreatedBy)";
            return await _db.ExecuteAsync(sql, salesTransact);
        }

        public async Task<int> Delete(Guid Id)
        {
            var sql = "UPDATE SalesTransaction SET IsDeleted = 1 WHERE Id = @Id";
            return await _db.ExecuteAsync(sql, new { Id });
        }


        public async Task<IEnumerable<Models.SalesTransact>> Search(string itemName, DateTime? salesDate, string paymentType)
        {
            var sql = "SELECT * FROM SalesTransaction WHERE IsDeleted = 0";
            var parameters = new DynamicParameters();

            if (!string.IsNullOrEmpty(itemName))
            {
                sql += " AND Item LIKE @ItemName";
                parameters.Add("ItemName", $"%{itemName}%");
            }

            if (salesDate.HasValue)
            {
                sql += " AND SalesDate = @SalesDate";
                parameters.Add("SalesDate", salesDate.Value);
            }

            if (!string.IsNullOrEmpty(paymentType))
            {
                sql += " AND PaymentType = @PaymentType";
                parameters.Add("PaymentType", paymentType);
            }

            return await _db.QueryAsync<Models.SalesTransact>(sql, parameters);
        }

    }
    }
