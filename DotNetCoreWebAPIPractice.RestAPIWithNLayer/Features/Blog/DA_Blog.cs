using DotNetCoreWebAPIPractice.RestAPIWithNLayer.Db;

namespace DotNetCoreWebAPIPractice.RestAPIWithNLayer.Features.Blog
{
    // Data Access Layer
    public class DA_Blog
    {
        private readonly AppDbContext _dbContext;

        public DA_Blog()
        {
            _dbContext = new AppDbContext();
        }

        public List<BlogModel> GetBlogs()
        {
            List<BlogModel> blogs = _dbContext.Blogs.ToList();

            return blogs;
        }
    }
}
