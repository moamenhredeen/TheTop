using System;
using System.Collections.Generic;
using System.Linq;
using ApplicationModel;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class OrderService : IOrderService
    {
        
        private AppDbContext _appDbContext;

        public OrderService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        // Order Service
        public int AddOreder(string userId)
        {
            var user = _appDbContext.ApplicationUsers.Where(user => user.Id == userId)
                               .Include(c => c.ShoppingCart)
                               .ThenInclude(a => a.Advertisements).Single();
                
                
            var totalPrice = user.ShoppingCart.Advertisements.Sum(a => a.Price);
            Order order = new Order
            {
                ApplicationUserId = userId,
                Advertisements = user.ShoppingCart.Advertisements,
                TotalPrice = totalPrice,
                Status = StatusOrderType.Cancel,
            };
            _appDbContext.Add(order);
            _appDbContext.SaveChanges();

            return order.OrderId;
        }

        public OrderDTO GetOrder(int orderId)
        {
            var order = _appDbContext.Orders.Where(order => order.OrderId == orderId)
                         .Include(a =>a.Advertisements).ThenInclude(a =>a.Category)
                         .Include(a => a.Advertisements).ThenInclude(a => a.Images)
                        .SingleOrDefault();

            return new OrderDTO { 
              Advertisements = order.Advertisements.Select(a => new AdvertisementDTO
              {
                  Name = a.Name,
                  CategoryName = a.Category.Name,
                  ImagesNames = a.Images.Select(imageName => imageName.Name),
                  Price = a.Price,
                  ID = a.AdvertisementId,
              }).ToList(),
              TotalPrice = order.TotalPrice,
              DiscountPrice = order.DiscountPrice == null ? 0 : (decimal)order.DiscountPrice,
              OrderId = order.OrderId
        };
        }

        public bool CheckoutOrder(string userId, int orderId)
        {
            var order = _appDbContext.Orders.Where(order => order.OrderId == orderId)
                         .SingleOrDefault();

            var bankAccount = _appDbContext.BankAccounts.Where(bank => bank.ApplicationUserId == userId)
                              .SingleOrDefault();
            var user = _appDbContext.ApplicationUsers.Where(user => user.Id == userId)
                               .Include(cart => cart.ShoppingCart).ThenInclude(a => a.Advertisements).Single();

            if(order.DiscountPrice > 0)
            {
                if( bankAccount.Balance > order.DiscountPrice)
                {
                    bankAccount.Balance -= (decimal)order.DiscountPrice;
                    user.ShoppingCart.Advertisements.Clear();
                    _appDbContext.SaveChanges();
                    return true;
                }
                
            }
            else
            {
                if (bankAccount.Balance > order.TotalPrice)
                {
                    bankAccount.Balance -= order.TotalPrice;
                    user.ShoppingCart.Advertisements.Clear();
                    _appDbContext.SaveChanges();
                    return true;
                }
            }

           
            return false;
        }

        public void  RemoveOrder(int orderId)
        {
            _appDbContext.Remove(new Order { OrderId = orderId });
            _appDbContext.SaveChanges();
        }

        public ICollection<OrderDTO> GetAllOrders()
        {
            var ordersList = _appDbContext.Orders.Include(a => a.Advertisements)
                            .ThenInclude(a => a.Category)
                            .Include(a => a.Advertisements).ThenInclude(a => a.Images).ToList();

            return ordersList.Select(order => new OrderDTO
            {
                  Advertisements = order.Advertisements.Select(a => new AdvertisementDTO
                  {
                      Name = a.Name,
                      CategoryName = a.Category.Name,
                      ImagesNames = a.Images.Select(imageName => imageName.Name),
                      Price = a.Price,
                      ID = a.AdvertisementId,
                  }).ToList(),
                  CreatedAt = order.CreatedAt,
                  TotalPrice = order.TotalPrice,
                  DiscountPrice = order.DiscountPrice,
                  OrderId = order.OrderId,
            }).ToList();
        }

        public ICollection<OrderDTO> SearchOrder(SearchDTO searchDto)
        {

            var ordersList = _appDbContext.Orders.Include(a => a.Advertisements)
                          .ThenInclude(a => a.Category)
                          .Include(a => a.Advertisements).ThenInclude(a => a.Images).ToList();

            if (searchDto.FromDate != new DateTime(0001, 01, 01))
            {
                ordersList = ordersList
               .Where(order => searchDto.FromDate <= order.CreatedAt.Date).ToList();
            }
            if (searchDto.ToDate != new DateTime(0001, 01, 01))
            {
                ordersList = ordersList
               .Where(order => order.CreatedAt.Date <= searchDto.ToDate).ToList();
            }


            return ordersList.Select(order => new OrderDTO
            {
                Advertisements = order.Advertisements.Select(a => new AdvertisementDTO
                {
                    Name = a.Name,
                    CategoryName = a.Category.Name,
                    ImagesNames = a.Images.Select(imageName => imageName.Name),
                    Price = a.Price,
                    ID = a.AdvertisementId,
                }).ToList(),
                CreatedAt = order.CreatedAt,
                TotalPrice = order.TotalPrice,
                DiscountPrice = order.DiscountPrice,
                OrderId = order.OrderId,
            }).ToList();
        }
    }
}