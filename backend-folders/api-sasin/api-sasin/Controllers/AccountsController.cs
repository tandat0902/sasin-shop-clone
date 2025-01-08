using api_sasin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Data;

namespace api_sasin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AccountsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            try
            {
                string query = "Select * from Accounts";

                DataTable myTable = new DataTable();
                string sqlDataSourse = _configuration.GetConnectionString("sasin_db");

                using(SqlConnection myConnection =  new SqlConnection(sqlDataSourse))
                {
                    await myConnection.OpenAsync(); 
                    using(SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        using(SqlDataReader myReader = await myCommand.ExecuteReaderAsync())
                        {
                            myTable.Load(myReader);
                        }
                    }
                }
                return new JsonResult(myTable);
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<JsonResult> Post(Accounts _accounts)
        {
            try
            {
                string query = @"Insert into Accounts
                (
                    AccountId, LoginName, Password, LastLogin,
                    DateCreated, Salt, Active
                )
                values 
                (
                    @AccountId, @LoginName, @Password, @LastLogin,
                    @DateCreated, @Salt, @Active
                )";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@AccountId", _accounts.AccountId);
                        myCommand.Parameters.AddWithValue("@LoginName", _accounts.LoginName);
                        myCommand.Parameters.AddWithValue("@Password", _accounts.Password);
                        myCommand.Parameters.AddWithValue("@LastLogin", _accounts.LastLogin);
                        myCommand.Parameters.AddWithValue("@DateCreated", _accounts.DateCreated);
                        myCommand.Parameters.AddWithValue("@Salt", _accounts.Salt);
                        myCommand.Parameters.AddWithValue("@Active", _accounts.Active);

                        // Thực thi lệnh
                        await myCommand.ExecuteNonQueryAsync();
                    }
                }
                return new JsonResult("Added successfully");
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<JsonResult> Put(Accounts _accounts)
        {
            try
            {
                string query = @"Update Accounts set 
                LoginName = @LoginName,
                Password = @Password,
                LastLogin = @LastLogin,
                Salt = @Salt,
                Active = @Active
                where AccountId = @AccountId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@AccountId", _accounts.AccountId);
                        myCommand.Parameters.AddWithValue("@LoginName", _accounts.LoginName);
                        myCommand.Parameters.AddWithValue("@Password", _accounts.Password);
                        myCommand.Parameters.AddWithValue("@LastLogin", _accounts.LastLogin);
                        myCommand.Parameters.AddWithValue("@Salt", _accounts.Salt);
                        myCommand.Parameters.AddWithValue("@Active", _accounts.Active);

                        // Thực thi lệnh
                        rowsAffected = await myCommand.ExecuteNonQueryAsync();
                    }
                }
                // Kiểm tra số hàng bị ảnh hưởng
                if (rowsAffected > 0)
                {
                    return new JsonResult("Updated successfully");
                }
                else
                {
                    return new JsonResult("No record found with the given AccountId");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Accounts _accounts)
        {
            try
            {
                string query = @"Delete from Accounts 
                where AccountId = @AccountId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@AccountId", _accounts.AccountId);

                        // Thực thi lệnh
                        rowsAffected = await myCommand.ExecuteNonQueryAsync();
                    }
                }
                // Kiểm tra số hàng bị ảnh hưởng
                if (rowsAffected > 0)
                {
                    return new JsonResult("Deleted successfully");
                }
                else
                {
                    return new JsonResult("No record found with the given AccountId");
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                // Lỗi ràng buộc khóa ngoại
                return new JsonResult(new { Error = "Cannot delete this account because it is referenced by other records." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Error = ex.Message });
            }
        }
    }
}
