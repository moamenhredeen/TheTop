using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface IAdvertisementService
    {
        void CreateNewAdvertisement(AdvertisementDTO advertisementsDto);
        void UpdateAdvertisement(AdvertisementDTO advertisementsDto);
        AdvertisementDTO GetAdvertisementById(int advertisementId);
        void RemoveAdvertisement(int advertisementId);
        IEnumerable<AdvertisementDTO> GetCustomerAdvertisements(string customerId);
        IEnumerable<AdvertisementDTO> GetAllAdvertisemensts();
        IEnumerable<AdvertisementDTO> SearchAdvertisemenst(SearchDTO searchDto);
    }
}