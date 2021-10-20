using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TheTop.Application.Dao;
using TheTop.Application.Entities;
using TheTop.Application.Services.DTOs;

namespace TheTop.Application.Services
{
    public class WorkService : IWorkService
    {
        private readonly AppDbContext _appDbContext;
        public WorkService(AppDbContext appDbContext) => _appDbContext = appDbContext;

        // Work Service
        public void StartWork(WorkDTO workDto)
        {
            if (FindStartDate(workDto.StartDate) == null)
            {
                _appDbContext.Add(new Work
                {
                    ApplicationUserId = workDto.ApplicationUserId,
                    StartDate = workDto.StartDate
                });
                _appDbContext.SaveChanges();
            }
        }

        public void EndWork(WorkDTO workDto)
        {
            var workStart = FindStartDate(workDto.EndDate);
            var workEnd = FindEndDate(workDto.EndDate);

            if (workStart != null && workEnd == null)
            {
                _appDbContext.Update(new Work
                {
                    WorkId = workStart.WorkId,
                    ApplicationUserId = workStart.ApplicationUserId,
                    StartDate = workStart.StartDate,
                    EndDate = workDto.EndDate
                });
                _appDbContext.SaveChanges();
            }
        }

        public Work FindStartDate(DateTime date)
        {
            var data = _appDbContext.Works.Where(w => w.StartDate.Date == date.Date).AsNoTracking().SingleOrDefault();

            return data;
        }

        public Work FindEndDate(DateTime date)
        {
            var data = _appDbContext.Works.Where(w => w.EndDate.Value.Date == date.Date).AsNoTracking()
                .SingleOrDefault();

            return data;
        }
    }
}