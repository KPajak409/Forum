using AutoMapper;
using Forum.Entities;
using Forum.Models;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Forum.Controllers
{
    [Authorize(Roles="Admin, Moderator")]
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ForumDbContext _dbContext;
        private readonly IBlackListService _blackListService;
        // GET: UserController
        public UserController(UserManager<User> userMenager, IMapper mapper, ForumDbContext dbContext, IBlackListService blackListService)
        {
            _userManager = userMenager;
            _mapper = mapper;
            _dbContext = dbContext;
            _blackListService = blackListService;
        }

        // GET: UserController/Details/5
        [AllowAnonymous]
        public ActionResult Details(int id)
        {
            var user = _dbContext
                .Users
                .Include(t => t.Topics)
                .FirstOrDefault(u => u.Id == id);
            var userDto = _mapper.Map<UserDto>(user);
            return View(userDto);
        }

        public async Task<ActionResult> BanUser(BanUserDto dto)
        {
            
            var user = await _userManager.GetUserAsync(User);
            dto.ModId = user.Id;
           
            return View("BanUser", dto);
        }
        public async Task<ActionResult> BanDetails(BanUserDto dto, int id)
        {
            await _blackListService.BanUser(dto, id);           
            
            return RedirectToAction("Details", new {id});
        }
    }
}
