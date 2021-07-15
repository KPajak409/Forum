using AutoMapper;
using Forum.Entities;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface ITopicService
    {
        IEnumerable<Topic> Get(int categoryId);
        Topic GetById(int categoryId, int topicId);
    }
    public class TopicService : ITopicService
    {
        private readonly ForumDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TopicService> _logger;

        public TopicService(ForumDbContext dbContext, IMapper mapper, ILogger<TopicService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public IEnumerable<Topic> Get(int categoryId)
        {
            var categories = GetByCategoryId(categoryId);

            var topics = categories.Topics;
            return topics;
        }


        public Topic GetById(int categoryId, int id)
        {

            var categories = GetByCategoryId(categoryId);

            var topic = categories
                .Topics
                .FirstOrDefault(c => c.Id == id);

            if(topic is null)
                throw new NotFoundException("get topic by id");
            return topic;

        }

        private Category GetByCategoryId(int categoryId)
        {
            var category = _dbContext
                .Categories
                .Include(t => t.Topics)
                    .ThenInclude(r => r.Responses)
                .FirstOrDefault(c => c.Id == categoryId);

            if (category is null)
                throw new NotFoundException($"Mordo No category with id = {categoryId}");
            return category;
        }
    }
}
