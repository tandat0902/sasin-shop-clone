using api_sasin.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api_sasin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public CategoriesController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<JsonResult> Get()
        {
            try
            {
                string query = "Select * From Categories";
                DataTable myTable = new DataTable();
                string sqlDataSource = _configuration.GetConnectionString("sasin_db");

                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        using (SqlDataReader myReader = await myCommand.ExecuteReaderAsync())
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
        public async Task<JsonResult> Post(Categories _categories)
        {
            try
            {
                string query = @"Insert into Categories
                (
                    CategoryId, CategoryName, Thumnail, Published, Description
                )
                values 
                (
                    @CategoryId, @CategoryName, @Thumnail, @Published, @Description
                )";
                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@CategoryId", _categories.CategoryId);
                        myCommand.Parameters.AddWithValue("@CategoryName", _categories.CategoryName);
                        myCommand.Parameters.AddWithValue("@Thumnail", _categories.Thumnail);
                        myCommand.Parameters.AddWithValue("@Published", _categories.Published);
                        myCommand.Parameters.AddWithValue("@Description", _categories.Description);

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
        public async Task<JsonResult> Put(Categories _categories)
        {
            try
            {
                string query = @"Update Categories set 
                CategoryName = @CategoryName,
                Thumnail = @Thumnail,
                Published = @Published,
                Description = @Description
                where CategoryId = @CategoryId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@CategoryId", _categories.CategoryId);
                        myCommand.Parameters.AddWithValue("@CategoryName", _categories.CategoryName);
                        myCommand.Parameters.AddWithValue("@Thumnail", _categories.Thumnail);
                        myCommand.Parameters.AddWithValue("@Published", _categories.Published);
                        myCommand.Parameters.AddWithValue("@Description", _categories.Description);

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
                    return new JsonResult("No record found with the given CategoryId");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Categories _categories)
        {
            try
            {
                string query = @"Delete from Categories 
                where CategoryId = @CategoryId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@CategoryId", _categories.CategoryId);

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
                    return new JsonResult("No record found with the given CategoryId");
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                // Lỗi ràng buộc khóa ngoại
                return new JsonResult(new { Error = "Cannot delete this category because it is referenced by other records." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Error = ex.Message });
            }
        }
    }
}
