﻿using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Practical19_DataAccessLayer.Interfaces;
using Practical19_DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Practical19_DataAccessLayer.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;


        public UserRepository(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }
        /// <summary>
        /// It login the with valid email password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> LoginUserAsync(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return new UserManagerResponse
                {
                    Message = "There is no user with this email try again!",
                    IsSuccess = false,
                };
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result)
            {
                return new UserManagerResponse
                {
                    Message = "Invalid Password try again!",
                    IsSuccess = false,
                };
            }
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim("Email", model.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id),

            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));
            }

            var keys = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Secret"]));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(2),
                signingCredentials: new SigningCredentials(keys, SecurityAlgorithms.HmacSha256)
                );
            string tokenAsString = new JwtSecurityTokenHandler().WriteToken(token);
            return new UserManagerResponse
            {
                Message = tokenAsString,
                IsSuccess = true,
                ExpireDate = token.ValidTo,
                Email = user.Email,
            };
        }
        /// <summary>
        /// It Register user in user table
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> RegisterUserAsync(RegisterViewModel model)
        {
            var email = await _userManager.FindByEmailAsync(model.Email);
            if (email != null)
            {
                return new UserManagerResponse
                {
                    Message = $"{email} already exist!"
                };
            }
            var IdetityUser = new IdentityUser()
            {
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(IdetityUser, model.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "Admin" });
                }

                if (!await _roleManager.RoleExistsAsync("User"))
                {
                    await _roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                }
                await _userManager.AddToRoleAsync(IdetityUser, "User");

                return new UserManagerResponse
                {
                    Message = "User Created Successfully!",
                    IsSuccess = true,
                };
            }

            return new UserManagerResponse
            {
                Message = "User not created try again!",
                IsSuccess = false,
                Errors = result.Errors.Select(e => e.Description)
            };
        }
        /// <summary>
        /// It Logout the user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<UserManagerResponse> LogoutUserAsync(Logout model)
        {
            return new UserManagerResponse
            {
                Message = "User Signout",
                IsSuccess = true,
            };
        }
        /// <summary>
        /// It returns all the user available in database with roles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RegisteredUser>> GetUsers()
        {

            List<RegisteredUser> list = new List<RegisteredUser>();

            foreach (var item in await _userManager.GetUsersInRoleAsync("Admin"))
            {
                list.Add(new RegisteredUser()
                {
                    Email = item.Email,
                    Role = "Admin",
                });
            }

            foreach (var item in await _userManager.GetUsersInRoleAsync("User"))
            {
                list.Add(new RegisteredUser()
                {
                    Email = item.Email,
                    Role = "User",
                });
            }


            return list;
        }
    }
}
