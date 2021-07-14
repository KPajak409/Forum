using AutoMapper;
using Forum.Entities;
using Forum.Exceptions;
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
             
    }
    public class CategoryService : ICategoryService
    {
        private readonly ForumDbContext _dbContext;
        //private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(ForumDbContext dbContext, ILogger<CategoryService> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        public IEnumerable<Category> Get()
        {
            var categories = _dbContext.Categories.ToList();

            return categories;
        }

        public Category GetById(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);
            if (category is null)
                throw new NotFoundException($"Mordo No category with id = {id}");
            return category;
        }

    }
}