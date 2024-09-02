using DotNetCoreWebAPIPractice.Db;
using DotNetCoreWebAPIPractice.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPIPractice.Controllers
{
    // This is a controller based on EF Core method.
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();
        // allows for a flexible way to represent different types of responses that an action can produce, such as JSON data, views, files, redirects, etc.
        [HttpGet]
        public IActionResult ReadBlog()
        {
            var blogs = _context.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.id == id);
            if (blog is null)
            {
                return NotFound("Error: Data not found!");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            _context.Blogs.Add(blog);
            var result = _context.SaveChanges();

            string message = result > 0 ? "Data created..." : "Failed to create your data";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var search_blog = _context.Blogs.FirstOrDefault(x => x.id == id);

            if (search_blog is null)
            {
                return NotFound("Error: Data not found!");
            }

            search_blog.title = blog.title;
            search_blog.author = blog.author;
            search_blog.blog_content = blog.blog_content;

            var result = _context.SaveChanges();

            string message = result > 0 ? "Updated Successfully..." : "Failed to update your data";
            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            var search_blog = _context.Blogs.FirstOrDefault(x => x.id == id);
            if (search_blog is null)
            {
                return NotFound("Error: Data not found!");
            }

            if (!string.IsNullOrEmpty(blog.title))
            {
                search_blog.title = blog.title;
            }

            if (!string.IsNullOrEmpty(blog.author))
            {
                search_blog.author = blog.author;
            }

            if (!string.IsNullOrEmpty(blog.blog_content))
            {
                search_blog.blog_content = blog.blog_content;
            }

            var result = _context.SaveChanges();

            string message = result > 0 ? "Successful patch..." : "Failed to patch!";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(x => x.id == id);
            if (blog is null)
            {
                return NotFound("Data not found!");
            }

            _context.Blogs.Remove(blog);
            var result = _context.SaveChanges();
            string message = result > 0 ? "Deleted successfully..." : "Failed to delete!";

            return Ok(message);
        }
    }
}
