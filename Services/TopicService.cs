﻿using AutoMapper;
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
        IEnumerable<Topic> Get(int categoryid);
        Topic GetById(int categoryid, int topicId);
        int Create(CreateTopicDto dto, int categoryid);
        void Delete(int categoryid, int id);
        void Update(CreateTopicDto dto, int categoryid, int id);
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
        public IEnumerable<Topic> Get(int categoryid)
        {
            var category = GetByCategoryId(categoryid);
            return category.Topics;
        }


        public Topic GetById(int categoryid, int id)
        {
            var category = GetByCategoryId(categoryid);

            var topic = category
                .Topics
                .FirstOrDefault(c => c.Id == id);

            if(topic is null)
                throw new NotFoundException($"No topic with id = {id}");
            return topic;

        }

        public int Create(CreateTopicDto dto, int categoryid)
        {
            var category = GetByCategoryId(categoryid);

            var topic = _mapper.Map<Topic>(dto);
            topic.Date = DateTime.Now;

            category.Topics.Add(topic);
            _dbContext.SaveChanges();

            return topic.Id;
        }

        public void Delete(int categoryid, int id)
        {
            var category = GetByCategoryId(categoryid);

            var topic = category
                .Topics
                .FirstOrDefault(c => c.Id == id);
            if (topic is null)
                throw new NotFoundException($"No topic with id = { id }");

            category.Topics.Remove(topic);
            _dbContext.SaveChanges();
        }

        public void Update(CreateTopicDto dto, int categoryid, int id)
        {
            var category = GetByCategoryId(categoryid);

            var topic = category
               .Topics
               .FirstOrDefault(c => c.Id == id);

            topic.Content = dto.Content;
            _dbContext.SaveChanges();
        }

        private Category GetByCategoryId(int categoryid)
        {
            var category = _dbContext
                .Categories
                .Include(t => t.Topics)
                    .ThenInclude(r => r.Responses)
                .Include(t => t.Topics)
                    .ThenInclude(u => u.Author)
                .FirstOrDefault(c => c.Id == categoryid);

            if (category is null)
                throw new NotFoundException($"Mordo No category with id = {categoryid}");
            return category;
        }

        
    }
}
