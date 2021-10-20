using System.Collections.Generic;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public interface IOrderService
    {
        int AddOreder(string userId);
        OrderDTO GetOrder(int orderId);
        bool CheckoutOrder(string userId, int orderId);
        void  RemoveOrder(int orderId);
        ICollection<OrderDTO> GetAllOrders();
        ICollection<OrderDTO> SearchOrder(SearchDTO searchDto);
    }
}