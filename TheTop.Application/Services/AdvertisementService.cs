using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
   public class AdvertisementService
    {
        private AppDbContext _appDbContext;

        public AdvertisementService(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public void CreateNewAdvertisement(AdvertisementDTO advertisementsDto)
        {
            Advertisement advertisementModel = new Advertisement()
            {
                Name = advertisementsDto.Name,
                Price = advertisementsDto.Price,
                CategoryId = advertisementsDto.CategoryId,
                ApplicationUserId = advertisementsDto.UserId,
            };
            foreach (var imageName in advertisementsDto.ImagesNames)
            {
                advertisementModel.Images.Add(new Image() {Name = imageName});
            }

            _appDbContext.Advertisements.Add(advertisementModel);
            _appDbContext.SaveChanges();
        }

        public void UpdateAdvertisement(AdvertisementDTO advertisementsDto)
        {
            Advertisement advertisementModel = _appDbContext.Advertisements.Where(a => a.AdvertisementId == advertisementsDto.ID)
                                              .Include(a => a.Images).Single();

            advertisementModel.AdvertisementId = advertisementsDto.ID;
            advertisementModel.Name = advertisementsDto.Name;
            advertisementModel.Price = advertisementsDto.Price;
            advertisementModel.CategoryId = advertisementsDto.CategoryId;
            advertisementModel.ApplicationUserId = advertisementsDto.UserId;
            advertisementModel.UpdatedAt = DateTime.Now;

           
            if(advertisementsDto.ImagesNames.ToList().Count > 0) {
                //foreach(var item in advertisementModel.Images)
                //{
                //    advertisementModel.Images.Remove(item);
                //}
                advertisementModel.Images.Clear();
            }
            advertisementModel.Images = advertisementsDto.ImagesNames.Select(imgName => new Image
            {
                Name = imgName
            }).ToList();

            _appDbContext.Update(advertisementModel);
            _appDbContext.SaveChanges();
        }

        public AdvertisementDTO GetAdvertisementById(int advertisementId)
        {
            var advertisement = _appDbContext.Advertisements.Where(el => el.AdvertisementId == advertisementId)
                               .Include(a => a.Images).Include(a => a.Category).SingleOrDefault();


            var advertisementDTO = new AdvertisementDTO()
            {
                ID = advertisement.AdvertisementId,
                Name = advertisement.Name,
                Price = advertisement.Price,
                CreatedAt = advertisement.CreatedAt,
                CategoryName = advertisement.Category.Name,
                ImagesNames = advertisement.Images.Select(el => el.Name).ToList()
            };
            return advertisementDTO;
        }

        public void RemoveAdvertisement(int advertisementId)
        {
            _appDbContext.Remove(new Advertisement() {AdvertisementId = advertisementId});
            _appDbContext.SaveChanges();
        }

        public IEnumerable<AdvertisementDTO> GetCustomerAdvertisements(string customerId)
        {
            var advertisementsList = _appDbContext.Advertisements.Where(a => a.ApplicationUserId == customerId)
                                     .Include(i => i.Images).Include(c => c.Category)
                                     .AsNoTracking().ToList();
                
            var advertisementsListDto = new List<AdvertisementDTO>();
            foreach (var advertisement in advertisementsList)
            {
                AdvertisementDTO advertisementsDto = new AdvertisementDTO
                {
                    ID = advertisement.AdvertisementId,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    CreatedAt = advertisement.CreatedAt,
                    CategoryName = advertisement.Category.Name,
                    ImagesNames = advertisement.Images.Select(el => el.Name),
                    CategoryId =advertisement.CategoryId
                };
                advertisementsListDto.Add(advertisementsDto);
            }

            return advertisementsListDto;
        }

        public IEnumerable<AdvertisementDTO> GetAllAdvertisemensts()
        {
            var advertisementsList = _appDbContext.Advertisements.Include(a => a.Images).Include(a =>a.Category).AsNoTracking().ToList();
            var advertisementsListDto = new List<AdvertisementDTO>();
            foreach (var advertisement in advertisementsList)
            {
                AdvertisementDTO advertisementsDto = new AdvertisementDTO
                {
                    ID = advertisement.AdvertisementId,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    CreatedAt = advertisement.CreatedAt,
                    CategoryName = advertisement.Category.Name,
                    ImagesNames = advertisement.Images.Select(el => el.Name)
                };
                advertisementsListDto.Add(advertisementsDto);
            }

            return advertisementsListDto;
        }

        public IEnumerable<AdvertisementDTO> SearchAdvertisemenst(SearchDTO searchDto)
        {
            var advertisementList = _appDbContext.Advertisements.AsNoTracking().Where(a => a.CategoryId == searchDto.CategoryId)
                                   .Include(a =>a.Images)
                                   .Include(a => a.Category).ToList();
             
            if(searchDto.FromDate != new DateTime(0001, 01, 01)){
                advertisementList = advertisementList
               .Where(advertisement => searchDto.FromDate >= advertisement.CreatedAt.Date).ToList();
            }
            if (searchDto.ToDate != new DateTime(0001, 01, 01))
            {
                advertisementList = advertisementList
               .Where(advertisement => advertisement.CreatedAt.Date <= searchDto.ToDate).ToList();
            }
            

            // TODO : refactoring 
            if (searchDto.Name != null)
            {
                advertisementList = advertisementList
                    .Where(a => a.Name.Contains(searchDto.Name))
                    .ToList();
            }

            if (searchDto.FromPrice != 0)
            {
                advertisementList = advertisementList
                    .Where(advertisement => advertisement.Price >= searchDto.FromPrice)
                    .ToList();
            }

            if (searchDto.ToPrice != 0)
            {
                advertisementList = advertisementList
                    .Where(advertisement => advertisement.Price <= searchDto.ToPrice)
                    .ToList();
            }

            return advertisementList
                .Select(advertisement => new AdvertisementDTO()
                {
                    ID = advertisement.AdvertisementId,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    CreatedAt = advertisement.CreatedAt,
                    CategoryName = advertisement.Category.Name,
                    ImagesNames = advertisement.Images.Select(image => image.Name)
                }).ToList();
        }


        public void AddCategory(CategoryDTO categoryDto)
        {
            _appDbContext.Categories.Add(new Category() {Name = categoryDto.Name});
            _appDbContext.SaveChanges();
        }

        public void UpdateCategory(CategoryDTO categoryDto)
        {
            _appDbContext.Categories.Update(new Category() {Name = categoryDto.Name, CategoryId = categoryDto.ID});
            _appDbContext.SaveChanges();
        }

        public CategoryDTO GetCategoryById(int categoryId)
        {
            var category = _appDbContext.Categories.Where(c => c.CategoryId == categoryId).SingleOrDefault();
            return new CategoryDTO { Name = category.Name, CreateAt = category.CreatedAt, ID = category.CategoryId };
        }
        public void RemoveCategory(int categoryId)
        {
            _appDbContext.Categories.Remove(new Category() {CategoryId = categoryId });
            _appDbContext.SaveChanges();
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categoryList = _appDbContext.Categories.AsNoTracking().ToList();
            return categoryList.Select(category => new CategoryDTO()
            {
                ID = category.CategoryId,
                Name = category.Name,
                CreateAt = category.CreatedAt
            });
        }


        //ShoppingCart Service 

        public void AddShoppingCart(int advertisementId , string userId)
        {
            var user = _appDbContext.ApplicationUsers.Where(a => a.Id == userId)
                .Include(a => a.ShoppingCart).ThenInclude(a => a.Advertisements).Single();

            var adv = _appDbContext.Advertisements.Where(a => a.AdvertisementId == advertisementId).Single();

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

        public void RemoveFromShoppingCart(int shoppingCartId)
        {
            _appDbContext.Remove(new ShoppingCart { ShoppingCartId = shoppingCartId });
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
                    Price = a.Price
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


        // Order Service
        public void AddOreder(string userId)
        {
            var user = _appDbContext.ApplicationUsers.Where(user => user.Id == userId)
                               .Include(c => c.ShoppingCart)
                               .ThenInclude(a => a.Advertisements).Single();
                
                
            var totalPrice = user.ShoppingCart.Advertisements.Sum(a => a.Price);

            _appDbContext.Add(new Order { 
               ApplicationUserId = userId,
               Advertisements = user.ShoppingCart.Advertisements, 
               TotalPrice = totalPrice
            });
            _appDbContext.SaveChanges();
        }

    }
}