using System.Linq;
using TheTop.Application.Dao;

namespace TheTop.Application.Services
{
    public class CompanyInformationService : ICompanyInformationService
    {
        private AppDbContext _appDbContext;

        public CompanyInformationService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        
        public string get(string key)
        {
            return _appDbContext.CompanyInformations
                .Single(el => el.Key == key).Value;
        }
    }
}