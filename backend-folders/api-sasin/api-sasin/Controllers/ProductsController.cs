using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace api_sasin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = "Select * From Products";
            DataTable myTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("sasin_db");
            SqlDataReader myReader;
            using (SqlConnection myConnection =  new SqlConnection(sqlDataSource))
            {
                myConnection.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                {
                    myReader = myCommand.ExecuteReader();
                    myTable.Load(myReader);
                    myReader.Close();
                    myConnection.Close();
                }
            }
            return new JsonResult(myTable);
        }
    }
}
