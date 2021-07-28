using AutoMapper;
using Forum.Entities;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Forum.Services
{
    public interface ICategoryService
    {
        IEnumerable<Category> Get();
        Category GetById(int? id);
        int Create(CreateOrUpdateCategoryDto dto);
        void Update(Category cat, int? id);
        int Update(CreateOrUpdateCategoryDto dto, int id);
        void Delete(int id);
        void DeleteConfirm(Category category);
    }

    public class CategoryService : ICategoryService
    {
        private readonly ForumDbContext _dbContext;
        private readonly IMapper _mapper;

        public CategoryService(ForumDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public IEnumerable<Category> Get()
        {
            var categories = _dbContext
                .Categories
                .Include(t => t.Topics)
                    .ThenInclude(r => r.Responses)
                .Include(t => t.Topics)
                    .ThenInclude(u => u.Author)
                .ToList();     
            return categories;
        }

        public Category GetById(int? id)
        {
            if (id is null)
                throw new NotFoundException("Id is null");
            var category = _dbContext
                .Categories
                .Include(t => t.Topics)
                    .ThenInclude(r => r.Responses)
                .Include(t => t.Topics)
                    .ThenInclude(u => u.Author)
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                throw new NotFoundException($"No category with id = {id}");
            return category;
        }

        public int Create(CreateOrUpdateCategoryDto dto)
        {
            var category = _mapper.Map<Category>(dto);
            _dbContext.Add(category);           
            _dbContext.SaveChanges();

            return category.Id;
        }
        public void Update(Category cat, int? id)
        {
            var category = GetById(id);
            category.Name = cat.Name;
            category.Description = cat.Description;
            _dbContext.SaveChanges();
        }

        public int Update(CreateOrUpdateCategoryDto dto, int id)
        {
            var category = GetById(dto.Id.Value);
            category.Name = dto.Name;
            category.Description = dto.Description;
            _dbContext.SaveChanges();

            return category.Id;
        }
        public void Delete(int id)
        {
            var category = _dbContext
                .Categories
                .FirstOrDefault(c => c.Id == id);

            if (category is null)
                throw new NotFoundException($"No category with id = {id}");

            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();         
        }

        public void DeleteConfirm(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}