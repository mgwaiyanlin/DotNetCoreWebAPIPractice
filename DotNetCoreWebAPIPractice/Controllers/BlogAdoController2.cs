using DotNetCoreWebAPIPractice.CustomServices;
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
    public class BlogAdoController2 : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM blogs";
            
            List<BlogModel> blogs = _adoDotNetService.Query<BlogModel>(query);

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetABlog(int id)
        {
            string query = "SELECT * FROM blogs WHERE id = @id";

            var blog = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@id", id));

            if (blog is null)
            {
                return NotFound("Data not found!");
            }


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

            int result = _adoDotNetService.Execute(query, 
                new AdoDotNetParameter("@title", blogModel.title),
                new AdoDotNetParameter("@author", blogModel.author),
                new AdoDotNetParameter("@blog_content", blogModel.blog_content)
                );

            string message = result > 0 ? "Successfully created!" : "Failed to create new data";

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

            int result = _adoDotNetService.Execute(query,
                new AdoDotNetParameter("@id", blogModel.id),
                new AdoDotNetParameter("@title", blogModel.title),
                new AdoDotNetParameter("@author", blogModel.author),
                new AdoDotNetParameter("@blog_content", blogModel.blog_content)
                );

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
            int result = _adoDotNetService.Execute(query, new AdoDotNetParameter("@id", id));
            string message = result > 0 ? "Data deleted successfully." : "Failed to delete your data!";

            return Ok(message);
        }
    }
}
