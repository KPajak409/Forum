using AutoMapper;
using Forum.Entities;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface IBlackListService
    {
        IEnumerable<BlackList> Get();
        BlackList GetBanById(int id);
        Task BanUser(BanUserDto dto, int id);
    }
    public class BlackListService : IBlackListService
    {
        private readonly ForumDbContext _dbContext;
        private readonly UserManager<User> _userManager;

        public BlackListService(ForumDbContext dbContext, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;        
        }

        public IEnumerable<BlackList> Get()
        {
            var blackList = _dbContext
                .BlackList
                .ToList();
            return blackList;
        }

        public BlackList GetBanById(int id)
        {
            var ban = _dbContext
                .BlackList
                .FirstOrDefault(b => b.Id == id);
            
            if(ban is null)
                throw new NotFoundException($"No ban with id = {id}");

            return ban;
        }

        public async Task BanUser(BanUserDto dto, int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            var ban = new BlackList()
            {
                ModId = dto.ModId,
                UserId = id,
                Reason = dto.Reason,
                AcquireDate = DateTime.Now                
            };
            ban.ExpireDate = ban.AcquireDate.AddDays(dto.Days);
            user.LockoutEnd = ban.ExpireDate;
            _dbContext.Add(ban);
            _dbContext.SaveChanges();
         }

    }
}
