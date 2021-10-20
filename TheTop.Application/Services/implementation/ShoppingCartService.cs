using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private AppDbContext _appDbContext;

        public ShoppingCartService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        //ShoppingCart Service 

        public void AddShoppingCart(int advertisementId , string userId)
        {
            var user = _appDbContext.ApplicationUsers.Where(a => a.Id == userId)
                .Include(a => a.ShoppingCart).ThenInclude(a => a.Advertisements).Single();

            var adv = _appDbContext.Advertisements.Single(a => a.AdvertisementId == advertisementId);

            if(user.ShoppingCart is null)
            {
                user.ShoppingCart = new ShoppingCart();

                user.ShoppingCart.Advertisements.Add(adv);

            }
            else{
                user.ShoppingCart.Advertisements.Add(adv);

            }
            _appDbContext.SaveChanges();
        }

        public void RemoveFromShoppingCart(int advertisementId, string userId)
        {
            var user = _appDbContext.ApplicationUsers.Where(a => a.Id == userId)
                .Include(a => a.ShoppingCart).ThenInclude(a => a.Advertisements).Single();
            var advertisement = user.ShoppingCart.Advertisements.Single(e => e.AdvertisementId == advertisementId);
            user.ShoppingCart.Advertisements.Remove(advertisement);
            _appDbContext.SaveChanges();
        }

        public ShoppingCartDTO GetAdvertisementsInShoppingCart(string userId)
        {
            var user = _appDbContext.ApplicationUsers.Where(user => user.Id == userId).
                              Include(c => c.ShoppingCart).ThenInclude(a => a.Advertisements).ThenInclude(c => c.Category).
                              Include(c => c.ShoppingCart).ThenInclude(a => a.Advertisements).ThenInclude(c => c.Images)
                              .Single();



            var shoppingCartDTO = new ShoppingCartDTO();

            if( ! (user.ShoppingCart is null))
            {
                shoppingCartDTO.Advertisements = user.ShoppingCart.Advertisements.Select(a => new AdvertisementDTO
                {
                    Name = a.Name,
                    CategoryName = a.Category.Name,
                    ImagesNames = a.Images.Select(imageName => imageName.Name),
                    Price = a.Price,
                    ID = a.AdvertisementId,
                }).ToList();

                shoppingCartDTO.ShoppingCartId = user.ShoppingCart.ShoppingCartId;
                shoppingCartDTO.TotalPrice = shoppingCartDTO.Advertisements.Sum(a => a.Price);
            }
           

            return shoppingCartDTO;
        }

        public int GetNumItemShoppingCart(string userId)
        {
            var x = _appDbContext.ApplicationUsers.Where(user => user.Id == userId).
                                     Include(c => c.ShoppingCart).ThenInclude(a =>a.Advertisements)
                                     .Single();
            if(x.ShoppingCart is null)
            {
                return 0;
            }

            return x.ShoppingCart.Advertisements.Count();
   
        }
    }
}