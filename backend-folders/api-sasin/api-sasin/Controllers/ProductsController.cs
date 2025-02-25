﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using api_sasin.Models;

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
        public async Task<JsonResult> Get()
        {
            try
            {
                string query = "Select * From Products";

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
        public async Task<JsonResult> Post(Products _products)
        {
            try
            {
                string query = @"Insert into Products
                (
                    ProductId, ProductName, UnitPrice, Discount, Description, Active, 
                    HomeFlag, DateCreated, DateModified, UnitsInStock, CategoryId
                )
                values 
                (
                    @ProductId, @ProductName, @UnitPrice, @Discount, @Description, @Active, 
                    @HomeFlag, @DateCreated, @DateModified, @UnitsInStock, @CategoryId
                )";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@ProductId", _products.ProductId);
                        myCommand.Parameters.AddWithValue("@ProductName", _products.ProductName);
                        myCommand.Parameters.AddWithValue("@UnitPrice", _products.UnitPrice);
                        myCommand.Parameters.AddWithValue("@Discount", _products.Discount);
                        myCommand.Parameters.AddWithValue("@Description", _products.Description);
                        myCommand.Parameters.AddWithValue("@Active", _products.Active);
                        myCommand.Parameters.AddWithValue("@HomeFlag", _products.HomeFlag);
                        myCommand.Parameters.AddWithValue("@DateCreated", _products.DateCreated);
                        myCommand.Parameters.AddWithValue("@DateModified", _products.DateModified);
                        myCommand.Parameters.AddWithValue("@UnitsInStock", _products.UnitsInStock);
                        myCommand.Parameters.AddWithValue("@CategoryId", _products.CategoryId);

                        // Thực thi lệnh
                        await myCommand.ExecuteNonQueryAsync();
                    }
                }
                return new JsonResult("Added successfully");
            }
            catch(Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<JsonResult> Put(Products _products)
        {
            try
            {
                string query = @"Update Products set 
                ProductName = @ProductName,
                UnitPrice = @UnitPrice,
                Discount = @Discount,
                Description = @Description,
                Active = @Active,
                HomeFlag = @HomeFlag,
                DateModified = @DateModified,
                UnitsInStock = @UnitsInStock,
                CategoryId = @CategoryId
                where ProductId = @ProductId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@ProductId", _products.ProductId);
                        myCommand.Parameters.AddWithValue("@ProductName", _products.ProductName);
                        myCommand.Parameters.AddWithValue("@UnitPrice", _products.UnitPrice);
                        myCommand.Parameters.AddWithValue("@Discount", _products.Discount);
                        myCommand.Parameters.AddWithValue("@Description", _products.Description);
                        myCommand.Parameters.AddWithValue("@Active", _products.Active);
                        myCommand.Parameters.AddWithValue("@HomeFlag", _products.HomeFlag);
                        myCommand.Parameters.AddWithValue("@DateModified", _products.DateModified);
                        myCommand.Parameters.AddWithValue("@UnitsInStock", _products.UnitsInStock);
                        myCommand.Parameters.AddWithValue("@CategoryId", _products.CategoryId);

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
                    return new JsonResult("No record found with the given ProductId");
                }
            }
            catch (Exception ex)
            {
                return new JsonResult($"Error: {ex.Message}");
            }
        }

        [HttpDelete]
        public async Task<JsonResult> Delete(Products _products)
        {
            try
            {
                string query = @"Delete from Products 
                where ProductId = @ProductId";

                string sqlDataSource = _configuration.GetConnectionString("sasin_db");
                int rowsAffected;
                using (SqlConnection myConnection = new SqlConnection(sqlDataSource))
                {
                    await myConnection.OpenAsync();
                    using (SqlCommand myCommand = new SqlCommand(query, myConnection))
                    {
                        // Thêm các tham số vào truy vấn
                        myCommand.Parameters.AddWithValue("@ProductId", _products.ProductId);

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
                    return new JsonResult("No record found with the given ProductId");
                }
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                // Lỗi ràng buộc khóa ngoại
                return new JsonResult(new { Error = "Cannot delete this product because it is referenced by other records." });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { Error = ex.Message });
            }
        }
    }
}
