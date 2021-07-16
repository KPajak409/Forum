using AutoMapper;
using Forum.Entities;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface IResponseService
    {
        IEnumerable<Response> Get( int topicid);
        Response GetById(int topicId, int id);
        int Create(CreateResponseDto dto, int topicid);
        void Delete(int topicid, int id);
        void Update(CreateResponseDto dto, int topicid, int id);

    }
    public class ResponseService : IResponseService
    {
        private readonly ForumDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ResponseService> _logger;

        public ResponseService(ForumDbContext dbContext, IMapper mapper, ILogger<ResponseService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        public IEnumerable<Response> Get( int topicid)
        {
            var topic = GetByTopicId(topicid);
            return topic.Responses;
        }

        public Response GetById( int topicid, int id)
        {
            var topic = GetByTopicId(topicid);
            var response = topic
                .Responses
                .FirstOrDefault(r => r.Id == id);
            if (response is null)
                throw new NotFoundException($"No response with id = { id }");
            return response;
        }

        public int Create(CreateResponseDto dto, int topicid)
        {
            var topic = GetByTopicId(topicid);

            var response = _mapper.Map<Response>(dto);
            response.Date = DateTime.Now;

            topic.Responses.Add(response);
            _dbContext.SaveChanges();

            return response.Id;
        }
        public void Delete(int topicid, int id)
        {
            var topic = GetByTopicId(topicid);

            var response = topic
                .Responses
                .FirstOrDefault(r => r.Id == id);
            if (response is null)
                throw new NotFoundException($"No response with id = { id }");

            topic.Responses.Remove(response);
            _dbContext.SaveChanges();
        }
        public void Update(CreateResponseDto dto, int topicid, int id)
        {
            var topic = GetByTopicId(topicid);

            var response = topic
               .Responses
               .FirstOrDefault(r => r.Id == id);

            response.Content = dto.Content;
            _dbContext.SaveChanges();
        }

        private Topic GetByTopicId(int topicid)
        {
            var topic = _dbContext
                .Topics
                .Include(r => r.Responses)
                    .ThenInclude(a => a.Author)
                .Include(c => c.Category)
                .FirstOrDefault(t => t.Id == topicid);

            if (topic is null)
                throw new NotFoundException($"No topic with id = {topicid}");
            return topic;
        }
    }
}
