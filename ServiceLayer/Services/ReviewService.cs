using DomainLayer.Models;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var review = await _reviewRepository.GetByIdAsync(id);
            await _reviewRepository.DeleteAsync(review);
        }

        public async Task<IEnumerable<Review>> GetAllAsync()
        {
            return await _reviewRepository.GetAllAsync();
        }
    }
}
