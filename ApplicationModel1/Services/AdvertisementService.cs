using ApplicationModel1.Dao;
using ApplicationModel1.Entities;
using ApplicationModel1.Services.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationModel1.Services
{
    class AdvertisementService
    {
        private ApplicationContext db;

        public AdvertisementService(ApplicationContext db)
        {
            this.db = db;
        }

        public void AddAdvertisement(AdvertisementsDTO dto)
        {

            Advertisement model = new Advertisement()
            {
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                CreateAt = DateTime.Now
            };
            db.Add(model);
            db.SaveChanges();
            foreach (var item in dto.ImagesNames)
            {
                Image modelImage = new Image();

                modelImage.Name = item +""+new Random().Next();
                modelImage.AdvertisementId = model.ID;
                db.Add(modelImage);
                db.SaveChanges();
            }
        }

        public void UpdateAdvertisement(AdvertisementsDTO dto)
        { 

            Advertisement model = new Advertisement()
            {   
                ID = dto.ID,
                Name = dto.Name,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                UserId = dto.UserId,
                UpdateAt = DateTime.Now
            };
            db.Update(model);
            db.SaveChanges();

            foreach (var item in dto.ImagesNames)
            {
                Image modelImage = new Image();

                modelImage.Name = item + "" + new Random().Next();
                modelImage.AdvertisementId = model.ID;
                db.Update(modelImage);
                db.SaveChanges();
            }
        }

        public AdvertisementsDTO GetAdvertisementById(int id)
        {
            var model = db.Advertisements.Where(a => a.ID == id).SingleOrDefault();

            AdvertisementsDTO dto = new AdvertisementsDTO
            {
                ID = model.ID,
                Name = model.Name,
                Price = model.Price,
                CreateAt = model.CreateAt,
                CategoryName = model.Category.Name,
            };

            //var modelIm = db.Images.Where(i => i.AdvertisementId == id).ToList();
                foreach(var img in model.Images)
            {
                dto.ImagesNames.Add(img.Name);
            }

            return dto;
        }

        public void RemoveAdvertisement(int id)
        {
            var modelA = db.Advertisements.Where(a => a.ID == id).SingleOrDefault();

            db.Remove(modelA);
            db.SaveChanges();
        }

        public IEnumerable<AdvertisementsDTO> GetAdvertisemenstsByCustomerId(int cusId)
        {
            var listModelAdv = db.Advertisements.Where(a => a.UserId == cusId).ToList();

            var listDTOAdv = new List<AdvertisementsDTO>();

                foreach(var obj in listModelAdv)
                {
                   AdvertisementsDTO dto = new AdvertisementsDTO
                    {
                          ID = obj.ID,
                          Name = obj.Name,
                          Price = obj.Price,
                          CreateAt = obj.CreateAt,
                          CategoryName = obj.Category.Name,
                    };

                   foreach(var img in obj.Images)
                     {
                         dto.ImagesNames.Add(img.Name);
                     }


                   listDTOAdv.Add(dto);
                }
            return listDTOAdv;
        }

        public IEnumerable<AdvertisementsDTO> GetAllAdvertisemensts()
        {
            var listModelAdv = db.Advertisements.AsNoTracking().ToList();

            var listDTOAdv = new List<AdvertisementsDTO>();

            foreach (var obj in listModelAdv)
            {
                AdvertisementsDTO dto = new AdvertisementsDTO
                {
                    ID = obj.ID,
                    Name = obj.Name,
                    Price = obj.Price,
                    CreateAt = obj.CreateAt,
                    CategoryName = obj.Category.Name,
                };

                foreach (var img in obj.Images)
                {
                    dto.ImagesNames.Add(img.Name);
                }


                listDTOAdv.Add(dto);
            }
            return listDTOAdv;
        }

        public IEnumerable<AdvertisementsDTO> SearchAdvertisemenst(SearchDTO dto)
        {

            var data = db.Advertisements.Where(a => a.CategoryId == dto.CategoryId).Include(c => c.Category).ToList();

            data = data.Where(a => a.CreateAt.Date <= dto.FromDate &&
                            a.CreateAt.Date >= dto.ToDate).ToList();
                 if ( dto.Name != null)
                   {
                      data = data.Where(a => a.Name.Contains(dto.Name)).ToList();
                   }
                 if(dto.FromPrice != 0)
                  {
                      data = data.Where(a => a.Price >= dto.FromPrice).ToList();
                  }
                 if (dto.ToPrice != 0)
                  {
                     data = data.Where(a => a.Price <= dto.ToPrice).ToList();
                  }

          

            var listDTOAdv = new List<AdvertisementsDTO>();

            foreach (var obj in data)
            {
                AdvertisementsDTO dtoAdv = new AdvertisementsDTO
                {
                    ID = obj.ID,
                    Name = obj.Name,
                    Price = obj.Price,
                    CreateAt = obj.CreateAt,
                    CategoryName = obj.Category.Name,
                };

                foreach (var img in obj.Images)
                {
                    dtoAdv.ImagesNames.Add(img.Name);
                }


                listDTOAdv.Add(dtoAdv);
            }
            return listDTOAdv;
        }


        // Service Category

        public void AddCategory(CategoryDTO dto)
        {
            Category model = new Category
            {
                ID = dto.ID,
                Name = dto.Name,
                CreateAt = DateTime.Now
            };

            db.Add(model);
            db.SaveChanges();
        }

        public void UpdateCategory(CategoryDTO dto)
        {
            Category model = new Category
            {
                ID = dto.ID,
                Name = dto.Name,
                CreateAt = DateTime.Now
            };

            db.Update(model);
            db.SaveChanges();
        }

        public void RemoveCategory(int id)
        {
           var model = db.Categories.Where(c => c.ID == id).SingleOrDefault();

            db.Remove(model);
            db.SaveChanges();
        }

        public IEnumerable<CategoryDTO> GetAllCategory()
        {
            var listModelCateg = db.Categories.ToList();

            var listDTOCateg = new List<CategoryDTO>();

            foreach(var item in listModelCateg)
            {
                CategoryDTO dto = new CategoryDTO
                {
                    ID = item.ID,
                    Name = item.Name,
                    CreateAt = item.CreateAt
                };
                listDTOCateg.Add(dto);
            }

            return listDTOCateg;
        }

    }
}
