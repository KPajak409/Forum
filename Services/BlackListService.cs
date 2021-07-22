using Forum.Entities;
using Forum.Exceptions;
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
    }
    public class BlackListService : IBlackListService
    {
        private readonly ForumDbContext _dbContext;

        public BlackListService(ForumDbContext dbContext)
        {
            _dbContext = dbContext;
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

    }
}
