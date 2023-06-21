using DomainLayer.Models;
using RepositoryLayer.Repositories.Interfaces;
using ServiceLayer.Services.Interfaces;

namespace ServiceLayer.Services
{
    public class WishlistService : IWishlistService
    {
        private readonly IWishlistRepository _wishlistRepository;

        public WishlistService(IWishlistRepository wishlistRepository)
        {
            _wishlistRepository = wishlistRepository;
        }

        public async Task CreateAsync(AppUser user)
        {
            Wishlist wishlist = new();

            wishlist.AppUserId = user.Id;

            await _wishlistRepository.CreateAsync(wishlist);
        }
    }
}
