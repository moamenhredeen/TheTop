using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TheTop.Application.Entities;
using TheTop.Application.Services;

namespace TheTop.Utils
{
    public static class ExtensionMethods
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<IShoppingCartService, ShoppingCartService>(); 
            services.AddScoped<ICouponService, CouponService>(); 
            services.AddScoped<ICategoryService, CategoryService>(); 
            services.AddScoped<IOrderService, OrderService>(); 
            services.AddScoped<IWorkService, WorkService>(); 
            services.AddScoped<ITaskService, TaskService>(); 
            services.AddScoped<IReportService, ReportService>(); 
            services.AddScoped<IContractService, ContractService>(); 
            services.AddScoped<ICompanyInformationService, CompanyInformationService>(); 
        }
    }

}