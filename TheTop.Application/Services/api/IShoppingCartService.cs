using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface IShoppingCartService
    {
        void AddShoppingCart(int advertisementId , string userId);
        void RemoveFromShoppingCart(int advertisementId, string userId);
        ShoppingCartDTO GetAdvertisementsInShoppingCart(string userId);
        int GetNumItemShoppingCart(string userId);
    }
}