using DomainLayer.Models;
using ServiceLayer.Helpers;

namespace ServiceLayer.ViewModels
{
    public class BlogVM
    {
        public Paginate<Blog> PaginateResult { get; set; }
        public int BlogCount { get; set; }
        public List<Blog> LastUploadedBlogs { get; set; }
        public List<Blog>? SearchedBlogs { get; set; }
    }
}
