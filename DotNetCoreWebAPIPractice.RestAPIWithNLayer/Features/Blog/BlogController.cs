using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPIPractice.RestAPIWithNLayer.Features.Blog
{
    // User Interface Layer
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult ReadBlog()
        {
            var blogs = _blBlog.GetBlogs();
            return Ok(blogs);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var blog = _blBlog.GetBlog(id);
            if (blog is null)
            {
                return NotFound("Error: Data not found!");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            var result = _blBlog.CreateBlog(blog);

            string message = result > 0 ? "Data created..." : "Failed to create your data";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            var search_blog = _blBlog.GetBlog(id);

            if (search_blog is null)
            {
                return NotFound("Error: Data not found!");
            }

            var result = _blBlog.UpdateBlog(id, search_blog);

            string message = result > 0 ? "Updated Successfully..." : "Failed to update your data";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var blog = _blBlog.GetBlog(id);

            if (blog is null)
            {
                return NotFound("Data not found!");
            }
            var result = _blBlog.DeleteBlog(id);
            string message = result > 0 ? "Deleted successfully..." : "Failed to delete!";

            return Ok(message);
        }
    }
}
}
