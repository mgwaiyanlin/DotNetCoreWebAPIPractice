using DotNetCoreWebAPIPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DotNetCoreWebAPIPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM blogs";
            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            //List<BlogModel> blogs = new List<BlogModel>();
            //foreach (DataRow data in dataTable.Rows)
            //{
            //    BlogModel blog = new BlogModel();
            //    blog.id = Convert.ToInt32(data["id"]);
            //    blog.title = Convert.ToString(data["title"]);
            //    blog.author = Convert.ToString(data["author"]);
            //    blog.blog_content = Convert.ToString((string)data["blog_content"]);
            //    blogs.Add(blog);
            //}

            // !NOTE: DIFFERENT WAY TO ITERATE DATA
            List<BlogModel> blogs = dataTable.AsEnumerable().Select(data => new BlogModel
            {
                id = Convert.ToInt32(data["id"]),
                title = Convert.ToString(data["title"]),
                author = Convert.ToString(data["author"]),
                blog_content = Convert.ToString((string)data["blog_content"])
            }).ToList();

            sqlConnection.Close();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetABlog(int id)
        {
            string query = "SELECT * FROM blogs WHERE id = @id";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();


            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("id", id);

            SqlDataAdapter adapter = new SqlDataAdapter(sqlCommand);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            if (dataTable.Rows.Count == 0)
            {
                return NotFound("Data not found!");
            }

            DataRow dataRow = dataTable.Rows[0];

            var blog = new BlogModel()
            {
                id = Convert.ToInt32(dataRow["id"]),
                title = Convert.ToString(dataRow["title"]),
                author = Convert.ToString(dataRow["author"]),
                blog_content = Convert.ToString(dataRow["blog_content"])
            };

            sqlConnection.Close();

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blogModel)
        {
            string query = @"INSERT INTO [dbo].[blogs]
                   ([title]
                   ,[author]
                   ,[blog_content])
             VALUES
                   (@title,
		           @author,
		           @blog_content)";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@title", blogModel.title);
            sqlCommand.Parameters.AddWithValue("@author", blogModel.author);
            sqlCommand.Parameters.AddWithValue("@blog_content", blogModel.blog_content);

            int result = sqlCommand.ExecuteNonQuery();

            string message = result > 0 ? "Successfully created!" : "Failed to create new data";

            sqlConnection.Close();

            return Ok(message);
        }

        [HttpPut]
        public IActionResult UpdateBlog(int id, BlogModel blogModel)
        {
            string query = @"UPDATE [dbo].[blogs]
                   SET [title] = @title
                      ,[author] = @author
                      ,[blog_content] = @blog_content
                 WHERE id = @id";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", blogModel.id);
            sqlCommand.Parameters.AddWithValue("@title", blogModel.title);
            sqlCommand.Parameters.AddWithValue("@author", blogModel.author);
            sqlCommand.Parameters.AddWithValue("@blog_content", blogModel.blog_content);

            int result = sqlCommand.ExecuteNonQuery();

            string message = result > 0 ? "Updated successfully in Ado." : "Failed to update in Ado.";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blogModel)
        {
            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blogModel.title))
            {
                conditions += "[title] = @title, ";
            }

            if (!string.IsNullOrEmpty(blogModel.author))
            {
                conditions += "[author] = @author, ";
            }

            if (!string.IsNullOrEmpty(blogModel.blog_content))
            {
                conditions += "[blog_content] = @blog_content, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("There is no data as do not match conditions!");
            }
            // For removing the last 'comma' and 'spacing' in the condition string.
            conditions = conditions.Substring(0, conditions.Length - 2);

            string query = $@"UPDATE [dbo].[blogs]
                   SET {conditions}
                 WHERE id = @id";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", blogModel.id);
            if (!string.IsNullOrEmpty(blogModel.title))
            {
                sqlCommand.Parameters.AddWithValue("@title", blogModel.title);
            }
            if (!string.IsNullOrEmpty(blogModel.author))
            {
                sqlCommand.Parameters.AddWithValue("@author", blogModel.author);
            }

            if (!string.IsNullOrEmpty(blogModel.blog_content))
            {
                sqlCommand.Parameters.AddWithValue("@blog_content", blogModel.blog_content);
            }

            int result = sqlCommand.ExecuteNonQuery();

            string message = result > 0 ? "Patched successfully in Ado." : "Failed to patch in Ado!";
            return Ok(message);
        }

        [HttpDelete]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[blogs] WHERE id = @id";

            SqlConnection sqlConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            sqlConnection.Open();

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@id", id);

            int result = sqlCommand.ExecuteNonQuery();
            string message = result > 0 ? "Data deleted successfully." : "Failed to delete your data!";

            return Ok(message);
        }
    }
}
