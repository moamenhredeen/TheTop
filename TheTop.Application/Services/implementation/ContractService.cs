using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class ContractService : IContractService
    {
        private readonly AppDbContext _appDbContext;
        public ContractService(AppDbContext appDbContext) => _appDbContext = appDbContext;
        
        // Contract Service
        public void CreateNewContract(ContractDTO contractDto)
        {
            _appDbContext.Add(new Contract {
             HourSalary = contractDto.HourSalary,
             MonthlyWorkingHours = contractDto.MonthlyWorkingHours,
             //ApplicationUserId = contractDto.ApplicationUserId,
            });

            _appDbContext.SaveChanges();
        }
        public void UpdateContract(ContractDTO contractDto)
        {
            _appDbContext.Update(new Contract
            {
                UpdatedAt = DateTime.Now,
                HourSalary = contractDto.HourSalary,
                MonthlyWorkingHours = contractDto.MonthlyWorkingHours,
                //ApplicationUserId = contractDto.ApplicationUserId, 
            });

            _appDbContext.SaveChanges();
        }
        public IEnumerable<ContractDTO> GetAllContract()
        {
            var contractsList = _appDbContext.Contracts.AsNoTracking().ToList();

            return contractsList.Select(contract => new ContractDTO
            {
                HourSalary = contract.HourSalary,
                MonthlyWorkingHours = contract.MonthlyWorkingHours,
                CreateAt = contract.CreatedAt
                
            });
        } 
    }
}