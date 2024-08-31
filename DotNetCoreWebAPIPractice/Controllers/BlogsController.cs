using DotNetCoreWebAPIPractice.Db;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPIPractice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _context = new AppDbContext();

        [HttpGet]
        public IActionResult ReadBlog()
        {
            var blogs = _context.Blogs.ToList();
            return Ok(blogs);
        }

        [HttpPost]
        public IActionResult CreateBlog()
        {
            return Ok("CREATE BLOG");
        }

        [HttpPut]
        public IActionResult UpdateBlog()
        {
            return Ok("PUT DATA");
        }

        [HttpPatch]
        public IActionResult PatchBlog()
        {
            return Ok("PATCH DATA");
        }

        [HttpDelete]
        public IActionResult DeleteBlog()
        {
            return Ok("DELETE DATA");
        }
    }
}
