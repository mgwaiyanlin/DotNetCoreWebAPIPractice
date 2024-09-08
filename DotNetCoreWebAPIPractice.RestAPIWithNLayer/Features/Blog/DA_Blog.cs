using DotNetCoreWebAPIPractice.RestAPIWithNLayer.Db;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

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

        public BlogModel GetBlog(int id)
        {
            var blog = _dbContext.Blogs.FirstOrDefault(x => x.id == id);
            return blog!;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            _dbContext.Blogs.Add(requestModel);
            var result = _dbContext.SaveChanges();

            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var search_blog = _dbContext.Blogs.FirstOrDefault(x => x.id == id);

            if (search_blog is null) return 0;

            search_blog.title = requestModel.title;
            search_blog.author = requestModel.author;
            search_blog.blog_content = requestModel.blog_content;

            var result = _dbContext.SaveChanges();

            return result;
        }

        public int DeleteBlog(int id)
        {
            var search_blog = _dbContext.Blogs.FirstOrDefault(x => x.id == id);

            if (search_blog is null) return 0;

            _dbContext.Blogs.Remove(search_blog);
            var result = _dbContext.SaveChanges();

            return result;
        }
    }
}
