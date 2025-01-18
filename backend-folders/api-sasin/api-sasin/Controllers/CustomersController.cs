using api_sasin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api_sasin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            try
            {
                string query = "Select * from Customers";

                DataTable myTable = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("sasin_db");

                using(SqlConnection myConnection =  new SqlConnection(sqlDataSource))
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
            catch(Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<JsonResult> Post(Customers _customers)
        {
            try
            {
                string query = @"Insert into Customers
                (
                    CustomerId, LastName, FirstName, Gender, Birthday, Email, Password,
                    PhoneNumber, Street, District, Ward, City, Salt, Action, DateCreated, LastLogin
                )
                values 
                (
                    @CustomerId, @LastName, @FirstName, @Gender, @Birthday, @Email, @Password,
                    @PhoneNumber, @Street, @District, @Ward, @City, @Salt, @Action, @DateCreated, @LastLogin
                )";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@CustomerId", _customers.CustomerId);
                        myCommand.Parameters.AddWithValue("@LastName", _customers.LastName);
                        myCommand.Parameters.AddWithValue("@FirstName", _customers.FirstName);
                        myCommand.Parameters.AddWithValue("@Gender", _customers.Gender);
                        myCommand.Parameters.AddWithValue("@Birthday", _customers.Birthday);
                        myCommand.Parameters.AddWithValue("@Email", _customers.Email);
                        myCommand.Parameters.AddWithValue("@Password", _customers.Password);
                        myCommand.Parameters.AddWithValue("@PhoneNumber", _customers.PhoneNumber);
                        myCommand.Parameters.AddWithValue("@Street", _customers.Street);
                        myCommand.Parameters.AddWithValue("@District", _customers.District);
                        myCommand.Parameters.AddWithValue("@Ward", _customers.Ward);
                        myCommand.Parameters.AddWithValue("@City", _customers.City);
                        myCommand.Parameters.AddWithValue("@Salt", _customers.Salt);
                        myCommand.Parameters.AddWithValue("@Action", _customers.Action);
                        myCommand.Parameters.AddWithValue("@DateCreated", _customers.DateCreated);
                        myCommand.Parameters.AddWithValue("@LastLogin", _customers.LastLogin);

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
        public async Task<JsonResult> Put(Customers _customers)
        {
            try
            {
                string query = @"Update Customers set 
                LastName = @LastName,
                FirstName = @FirstName,
                Gender = @Gender,
                Birthday = @Birthday,
                Email = @Email,                
                Password = @Password,
                PhoneNumber = @PhoneNumber,
                Street = @Street,
                District = @District,
                Ward = @Ward,
                City = @City,
                Action = @Action,               
                DateCreated = @DateCreated,
                Salt = @Salt,
                LastLogin = @LastLogin
                where CustomerId = @CustomerId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@CustomerId", _customers.CustomerId);
                        myCommand.Parameters.AddWithValue("@LastName", _customers.LastName);
                        myCommand.Parameters.AddWithValue("@FirstName", _customers.FirstName);
                        myCommand.Parameters.AddWithValue("@Gender", _customers.Gender);
                        myCommand.Parameters.AddWithValue("@Birthday", _customers.Birthday);
                        myCommand.Parameters.AddWithValue("@Email", _customers.Email);
                        myCommand.Parameters.AddWithValue("@Password", _customers.Password);
                        myCommand.Parameters.AddWithValue("@PhoneNumber", _customers.PhoneNumber);
                        myCommand.Parameters.AddWithValue("@Street", _customers.Street);
                        myCommand.Parameters.AddWithValue("@District", _customers.District);
                        myCommand.Parameters.AddWithValue("@Ward", _customers.Ward);
                        myCommand.Parameters.AddWithValue("@City", _customers.City);
                        myCommand.Parameters.AddWithValue("@Salt", _customers.Salt);
                        myCommand.Parameters.AddWithValue("@Action", _customers.Action);
                        myCommand.Parameters.AddWithValue("@DateCreated", _customers.DateCreated);
                        myCommand.Parameters.AddWithValue("@LastLogin", _customers.LastLogin);

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
                    return new JsonResult("No record found with the given CustomerId");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Customers _customers)
        {
            try
            {
                string query = @"Delete from Customers 
                where CustomerId = @CustomerId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@CustomerId", _customers.CustomerId);

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
                    return new JsonResult("No record found with the given CustomerId");
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
