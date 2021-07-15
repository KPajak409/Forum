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
    public interface ICategoryService
    {
        IEnumerable<Category> Get();
        Category GetById(int id);
        int Create(CreateCategoryDto dto);
             
    }
    public class CategoryService : ICategoryService
    {
        private readonly ForumDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ForumDbContext dbContext, ILogger<CategoryService> logger, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _mapper = mapper;
        }

        public IEnumerable<Category> Get()
        {
            var categories = _dbContext
                .Categories
                .Include(t => t.Topics)
                    .ThenInclude(r => r.Responses)
                .ToList();     
            return categories;
        }

        public Category GetById(int id)
        {
            var category = _dbContext
                .Categories
                .Include(t => t.Topics)
                    .ThenInclude(r => r.Responses)
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                throw new NotFoundException($"Mordo No category with id = {id}");
            return category;
        }

        public int Create(CreateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _dbContext.Add(category);
            _dbContext.SaveChanges();

            return category.Id;
        }
    }
}