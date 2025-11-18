using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlayground.Database.AppDbContextModels;
using ApiPlayground.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ApiPlayground.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsAdoDotNetController : ControllerBase
    {
        private readonly string _connectionString = "Server=.;Database=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
        
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> lts = new List<BlogViewModel>();
            
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            
            string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0 ORDER BY BlogId DESC";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                var item = new BlogViewModel()
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = reader["BlogTitle"].ToString()!,
                    Author = reader["BlogAuthor"].ToString()!,
                    Content = reader["BlogContent"].ToString()!,
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                };

                lts.Add(item);
            }
            
            connection.Close();
            
            return Ok(lts);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = new BlogViewModel();
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId AND DeleteFlag = 0";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);

            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                item = new BlogViewModel()
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = reader["BlogTitle"].ToString()!,
                    Author = reader["BlogAuthor"].ToString()!,
                    Content = reader["BlogContent"].ToString()!,
                    DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"]),
                };
            }
            
            connection.Close();
            return Ok(item);
        }
        
        [HttpPost()]
        public IActionResult CreateBlog(BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent, DeleteFlag)
                VALUES (@BlogTitle, @BlogAuthor, @BlogContent, 0);";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            return Ok(result > 0 ? "Saving successfully." : "Saving failed.");
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogViewModel blog)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = @"
                UPDATE Tbl_Blog 
                SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent, DeleteFlag = 0
                WHERE BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blog.Content);
            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            
            return Ok(result > 0 ? "Updating successfully." : "Updating failed.");
        }
        
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogViewModel blog)
        {
            string conditions = "";
            if (!string.IsNullOrEmpty(blog.Title))
            {
                conditions += " BlogTitle = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blog.Author))
            {
                conditions += " BlogAuthor = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blog.Content))
            {
                conditions += " BlogContent = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return BadRequest("Invalid parameters.");
            }

            conditions = conditions.Substring(0, conditions.Length - 2);
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();

            string query = $"UPDATE Tbl_Blog SET {conditions} WHERE BlogId = @BlogId";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            if (!string.IsNullOrEmpty(blog.Title))
                cmd.Parameters.AddWithValue("@BlogTitle", blog.Title);
            if (!string.IsNullOrEmpty(blog.Author))
                cmd.Parameters.AddWithValue("@BlogAuthor", blog.Author);
            if (!string.IsNullOrEmpty(blog.Content))
                cmd.Parameters.AddWithValue("@BlogContent", blog.Content);

            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            
            return Ok(result > 0 ? "Updating successfully." : "Updating failed.");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            connection.Open();
            
            string query = "UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";
            
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@BlogId", id);
            
            int result = cmd.ExecuteNonQuery();
            
            connection.Close();
            
            return Ok(result > 0 ? "Deleting successfully." : "Deleting failed.");
        }
    }
}
