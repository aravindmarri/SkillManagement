using AutoMapper;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using Models.DBModels;
using Models.DTOModels;
using Models.ViewModels;
using Services.Internfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Services
{
    public class MainService : IMainService
    {

        private IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IBaseRepository<User> _userRepository;

        public MainService(IConfiguration configuration, IMapper mapper, UserManager<IdentityUser> userManager, IBaseRepository<User> userRepository)
        {
            _config = configuration;
            _userManager = userManager;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<LoginToken> Login(string UserName, string Password)
        {
            try
            {
                // Log the exception
                // Call your service method here passing loginViewModel and return User object if the login is successful

                var user = await _userManager.FindByEmailAsync(UserName);
                if (user != null && await _userManager.CheckPasswordAsync(user, Password))
                {
                    var result = _userRepository.FindBy(x => x.AspNetUserId == user.Id);
                    var claims = new Claim[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                    var token = new JwtSecurityToken(
                    issuer: _config["Jwt:Issuer"],
                    audience: _config["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddDays(Convert.ToDouble(_config["Jwt:ExpireDays"]))
                    );
                    var mapUser = _mapper.Map<UserDto>(result);
                    return new LoginToken
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo,
                        UserId = result.Id,
                        IsAuthorized = true,
                        User = mapUser
                    };
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {


                return null;
            }
        }

        public async Task<IdentityUser> GetUserById(string Id)
        {
            try
            {
            var user = await _userManager.FindByIdAsync(Id);
                if (user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
