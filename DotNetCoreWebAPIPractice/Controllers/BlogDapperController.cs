using Dapper;
using DotNetCoreWebAPIPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace DotNetCoreWebAPIPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetBlogs()
        {
            string query = "SELECT * FROM blogs";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            List<BlogModel> blogs = dbConnection.Query<BlogModel>(query).ToList();

            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult GetABlog(int id)
        {
            var blog = FindbyId(id);

            if (blog == null)
            {
                return NotFound("This blog is not found!");
            }

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[blogs]
                   ([title]
                   ,[author]
                   ,[blog_content])
             VALUES
                   (@title,
		           @author,
		           @blog_content)";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = dbConnection.Execute(query, blog);

            string message = result > 0 ? "A new blog data was created successfully." : "Failed to create a new data";
            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var find_blog = FindbyId(id);

            if (find_blog == null)
            {
                return NotFound("This blog is not found!");
            }

            blog.id = id;

            string query = @"UPDATE [dbo].[blogs]
                   SET [title] = @title
                      ,[author] = @author
                      ,[blog_content] = @blog_content
                 WHERE id = @id";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            int result = dbConnection.Execute(query, blog);

            string message = result > 0 ? "Updated successfully." : "Failed to update";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var search_blog = FindbyId(id);
            if (search_blog == null)
            {
                return NotFound("Data Not Found!");
            }

            string conditions = string.Empty;

            if (!string.IsNullOrEmpty(blog.title))
            {
                conditions += "[title] = @title, ";
            }

            if (!string.IsNullOrEmpty(blog.author))
            {
                conditions += "[author] = @author, ";
            }

            if (!string.IsNullOrEmpty(blog.blog_content))
            {
                conditions += "[blog_content] = @blog_content, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("There is no data as do not match conditions!");
            }
            // For removing the last 'comma' and 'spacing' in the condition string.
            conditions = conditions.Substring(0, conditions.Length -2);

            blog.id = id;

            string query = $@"UPDATE [dbo].[blogs]
                   SET {conditions}
                 WHERE id = @id";

            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            int result = dbConnection.Execute(query, blog);

            string message = result > 0 ? "Patched successfully." : "Failed to patch data";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = FindbyId(id);

            if (blog == null)
            {
                return NotFound("The blog is not found in the system!");
            }
            string query = @"DELETE FROM [dbo].[blogs] WHERE id = @id";

            using IDbConnection connection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            int result = connection.Execute(query, new BlogModel() { id = id });

            string message = result > 0 ? "deleted successfully" : "failed to delete";

            return Ok(message);
        }

        private BlogModel? FindbyId(int id)
        {
            string query = "SELECT * FROM blogs WHERE id = @id";
            using IDbConnection dbConnection = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

            var blog = dbConnection.Query<BlogModel>(query, new BlogModel { id = id }).FirstOrDefault();

            return blog;
        }
    }
}
