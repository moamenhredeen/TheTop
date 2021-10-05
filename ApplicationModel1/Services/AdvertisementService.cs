using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationModel1.Dao;
using ApplicationModel1.Entities;
using ApplicationModel1.Services.DTOs;
using Microsoft.EntityFrameworkCore;

namespace ApplicationModel1.Services
{
    class AdvertisementService
    {
        private AppDbContext _appDbContext;

        public AdvertisementService(AppDbContext appDbContext) => _appDbContext = appDbContext;

        public void CreateNewAdvertisement(AdvertisementsDTO advertisementsDto)
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

        public void UpdateAdvertisement(AdvertisementsDTO advertisementsDto)
        {
            Advertisement advertisementModel = new Advertisement()
            {
                AdvertisementId = advertisementsDto.ID,
                Name = advertisementsDto.Name,
                Price = advertisementsDto.Price,
                CategoryId = advertisementsDto.CategoryId,
                ApplicationUserId = advertisementsDto.UserId,
                UpdatedAt = DateTime.Now
            };
            foreach (var imageName in advertisementsDto.ImagesNames)
            {
                advertisementModel.Images.Add(new Image() {Name = imageName});
            }

            _appDbContext.Update(advertisementModel);
            _appDbContext.SaveChanges();
        }

        public AdvertisementsDTO GetAdvertisementById(int advertisementId)
        {
            var advertisement = _appDbContext.Advertisements
                .AsNoTracking()
                .SingleOrDefault(el => el.AdvertisementId == advertisementId);


            var advertisementDTO = new AdvertisementsDTO()
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

        public IEnumerable<AdvertisementsDTO> GetCustomerAdvertisements(string customerId)
        {
            var advertisementsList = _appDbContext.ApplicationUsers.AsNoTracking()
                .Include(applicationUser => applicationUser.Advertisements)
                .FirstOrDefault(applicationUser => applicationUser.Id == customerId)
                ?.Advertisements;
            var advertisementsListDto = new List<AdvertisementsDTO>();
            foreach (var advertisement in advertisementsList)
            {
                AdvertisementsDTO advertisementsDto = new AdvertisementsDTO
                {
                    ID = advertisement.AdvertisementId,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    CreatedAt = advertisement.CreatedAt,
                    CategoryName = advertisement.Category.Name,
                    ImagesNames = advertisement.Images.Select(el => el.Name)
                };
            }

            return advertisementsListDto;
        }

        public IEnumerable<AdvertisementsDTO> GetAllAdvertisemensts()
        {
            var advertisementsList = _appDbContext.Advertisements.AsNoTracking().ToList();
            var advertisementsListDto = new List<AdvertisementsDTO>();
            foreach (var advertisement in advertisementsList)
            {
                AdvertisementsDTO advertisementsDto = new AdvertisementsDTO
                {
                    ID = advertisement.AdvertisementId,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    CreatedAt = advertisement.CreatedAt,
                    CategoryName = advertisement.Category.Name,
                    ImagesNames = advertisement.Images.Select(el => el.Name)
                };
            }

            return advertisementsListDto;
        }

        public IEnumerable<AdvertisementsDTO> SearchAdvertisemenst(SearchDTO searchDto)
        {
            var advertisementList = _appDbContext.Advertisements.AsNoTracking()
                .Include(advertisement => advertisement.Category)
                .Where(Advertisement => Advertisement.CategoryId == searchDto.CategoryId)
                .ToList();

            advertisementList = advertisementList
                .Where(advertisement => searchDto.FromDate >= advertisement.CreatedAt.Date &&
                                        advertisement.CreatedAt.Date <= searchDto.ToDate).ToList();

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
                .Select(advertisement => new AdvertisementsDTO()
                {
                    ID = advertisement.AdvertisementId,
                    Name = advertisement.Name,
                    Price = advertisement.Price,
                    CreatedAt = advertisement.CreatedAt,
                    CategoryName = advertisement.Category.Name,
                    ImagesNames = advertisement.Images.Select(image => image.Name)
                });
        }


        public void AddCategory(CategoryDTO categoryDto)
        {
            _appDbContext.Categories.Add(new Category() {Name = categoryDto.Name});
            _appDbContext.SaveChanges();
        }

        public void UpdateCategory(CategoryDTO categoryDto)
        {
            _appDbContext.Categories.Add(new Category() {Name = categoryDto.Name, CategoryId = categoryDto.ID});
            _appDbContext.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
            _appDbContext.Categories.Remove(new Category() {CategoryId = id});
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
    }
}