using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationModel;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
   public class AdvertisementService : IAdvertisementService
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
               .Where(advertisement => searchDto.FromDate <= advertisement.CreatedAt.Date).ToList();
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
    }
}