using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPIPractice.RestAPIWithNLayer.Features.Blog
{
    // Business Logic Layer for logical (+, -, *, %) and data input validation
    public class BL_Blog
    {
        private readonly DA_Blog _daBlog;

        public BL_Blog()
        {
            _daBlog = new DA_Blog();
        }

        public List<BlogModel> GetBlogs()
        {
            List<BlogModel> blogs = _daBlog.GetBlogs();

            return blogs;
        }

        public BlogModel GetBlog(int id)
        {
            var blog = _daBlog.GetBlog(id);
            return blog!;
        }

        public int CreateBlog(BlogModel requestModel)
        {
            var result = _daBlog.CreateBlog(requestModel);

            return result;
        }

        public int UpdateBlog(int id, BlogModel requestModel)
        {
            var result = _daBlog.UpdateBlog(id, requestModel);

            return result;
        }

        public int DeleteBlog(int id)
        {
            var result = _daBlog.DeleteBlog(id);

            return result;
        }
    }
}
