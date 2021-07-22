using AutoMapper;
using Forum.Authorization;
using Forum.Entities;
using Forum.Exceptions;
using Forum.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Forum.Services
{
    public interface IAccountService
    {
        User GetById(int userId);
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginUserDto dto);
        void ChangeRole(int userId, int roleId);
        void Update(UpdateUserDto dto, int userId);
        int BanUser(int userId, BanUserDto dto);
    }
    public class AccountService : IAccountService
    {
        private readonly ForumDbContext _dbContext;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly AuthenticationSettings _authenticationSettings;
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;
        private readonly IMapper _mapper;
        public AccountService(ForumDbContext dbContext, IPasswordHasher<User> passwordHasher, 
            AuthenticationSettings authenticationSettings, IAuthorizationService authorizationService,
            IUserContextService userContextService, IMapper mapper)
        {
            _dbContext = dbContext;
            _passwordHasher = passwordHasher;
            _authenticationSettings = authenticationSettings;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
            _mapper = mapper;
        }
        public User GetById(int userId)
        {
            var user = _dbContext
                .Users
                .FirstOrDefault(u => u.Id == userId);

            if (user is null)
                throw new NotFoundException($"No user with id={userId}");

            return user;
        }

        public void RegisterUser(RegisterUserDto dto)
        {
            var newUser = new User()
            {
                Email = dto.Email,
                DateOfBirth = dto.DateOfBirth,
                Username = dto.Username,
                Name = dto.Name,
                SurName = dto.SurName,
                RoleId = dto.RoleId
            };

            var hashedPassword = _passwordHasher.HashPassword(newUser, dto.Password);
            newUser.HashedPassword = hashedPassword;

            _dbContext.Users.Add(newUser);
            _dbContext.SaveChanges();
        }

        public string GenerateJwt(LoginUserDto dto)
        {
            var user = _dbContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == dto.Email);
            if (user is null)
                throw new BadRequestException("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(user, user.HashedPassword, dto.Password);
            if (result == PasswordVerificationResult.Failed)
                throw new BadRequestException("Invalid username or password");

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
                new Claim("DateOfBirth", user.DateOfBirth.ToString("yyyy-MM-dd")),
                new Claim("Username", user.Username),
                new Claim("Email", user.Email)
            };

            if (user.Name is not null && user.SurName is not null)
            {
                claims.Add(
                    new Claim(ClaimTypes.Name, $"{user.Name} {user.SurName}")
                    );
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: credentials);

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);

        }

        public void ChangeRole(int userId, int roleId)
        {
            var user = GetById(userId);
            var role = _dbContext.Roles.FirstOrDefault(r => r.Id == roleId);

            if(role is null)
                throw new NotFoundException($"Role with id = {roleId} doesn't exist");
            if(role.Name == "Admin")
                throw new NotAuthorizedException("You can't set this role");

            user.RoleId = roleId;
        }

        public void Update(UpdateUserDto dto, int userId)
        {
            var user = GetById(userId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, user, new UserOperationRequirement(ResourceOperation.Update)).Result;
            if (!authorizationResult.Succeeded)
                throw new NotAuthorizedException("You are not authorized");


            user.Name = dto.Name;
            user.SurName = dto.SurName;
            _dbContext.SaveChanges();
        }

        public int BanUser(int userId, BanUserDto dto)
        {
            var modId = _userContextService.GetUserId;
            var ban = _mapper.Map<BlackList>(dto);

            ban.ModId = modId;
            ban.UserId = userId;
            ban.AcquireDate = DateTime.Now;
            ban.ExpireDate = DateTime.Now.AddDays(dto.Days);
            
            _dbContext.BlackList.Add(ban);
            _dbContext.SaveChanges();

            return ban.Id;
        }
    }
}
