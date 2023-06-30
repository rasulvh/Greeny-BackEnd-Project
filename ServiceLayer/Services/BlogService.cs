using DomainLayer.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RepositoryLayer.Repositories;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Helpers;
using ServiceLayer.Services.Interfaces;
using ServiceLayer.ViewModels.Admin.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class BlogService : IBlogService
    {
        private readonly IBlogRepository _blogRepository;
        private readonly IWebHostEnvironment _env;

        public BlogService(IBlogRepository blogRepository,
                           IWebHostEnvironment env)
        {
            _blogRepository = blogRepository;
            _env = env;
        }

        public async Task CreateAsync(BlogCreateVM request)
        {
            string image = string.Empty;

            string fileName = Guid.NewGuid().ToString() + "_" + request.Image.FileName;
            await request.Image.SaveFileAsync(fileName, _env.WebRootPath, "images/blog");

            image = fileName;

            Blog blog = new()
            {
                Title = request.Title,
                Text = request.Text,
                Image = image
            };

            await _blogRepository.CreateAsync(blog);
        }

        public async Task DeleteAsync(int id)
        {
            var blog = await _blogRepository.GetByIdAsync(id);

            await _blogRepository.DeleteAsync(blog);

            string path = Path.Combine(_env.WebRootPath, "images/blog", blog.Image);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task EditAsync(int id, BlogEditVM request)
        {
            var blog = await _blogRepository.GetByIdAsync(id);

            if (request.NewImage != null)
            {
                string fileName = Guid.NewGuid().ToString() + "_" + request.NewImage.FileName;
                blog.Image = fileName;
                await request.NewImage.SaveFileAsync(fileName, _env.WebRootPath, "images/blog");
            }

            blog.Text = request.Text;
            blog.Title = request.Title;

            await _blogRepository.UpdateAsync(blog);
        }

        public async Task<IEnumerable<Blog>> GetAllAsync()
        {
            return await _blogRepository.GetAllAsync();
        }

        public async Task<Blog> GetByIdAsync(int id)
        {
            return await _blogRepository.GetByIdAsync(id);
        }

        public async Task<int> GetCountAsync()
        {
            var blogs = await _blogRepository.GetAllAsync();

            return blogs.Count();
        }

        public async Task<List<Blog>> GetPaginatedDatasAsync(int page, int take)
        {
            return await _blogRepository.GetPaginatedDatasAsync(page, take);
        }

        public async Task<List<Blog>> GetSearchedBlogs(string searchText = null)
        {
            var blogs = await _blogRepository.GetAllAsync();

            List<Blog> searchBlogs = new();

            foreach (var item in blogs)
            {
                if (item.Title.ToLower().Contains(searchText))
                {
                    searchBlogs.Add(item);
                }
            }

            return searchBlogs;
        }
    }
}
