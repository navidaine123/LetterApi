using AutoMapper;
using Helpers;
using Helpers.Common;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository;
using Services.Dto;
using Services.Shared;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Test.Models.UserModels;

namespace Services.UserServices
{
    public interface IUserService
    {
        Task<ICollection<UserDto>> GetAllUsersAsync();

        Task<LogInResponseDto> GenerateTokenAsync(LogInDto logIn);

        Task<User> RegisterAsync(RegisterDto registerDto);

        Guid GetUSerIDFromUserClaims(IEnumerable<Claim> claims);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration config)
        {
            _unitOfWork = unitOfWork;
            _userRepository = _unitOfWork.UserRepository;
            _mapper = mapper;
            _config = config;
        }

        public async Task<ICollection<UserDto>> GetAllUsersAsync()
        {
            return _mapper.Map<List<UserDto>>(await _userRepository.GetAllAsync());
        }

        public async Task<LogInResponseDto> GenerateTokenAsync(LogInDto logIn)
        {
            LogInResponseDto response = null;

            var user = await _userRepository.GetUserByUserNameAsync(logIn.UserName);
            if (user == null)
                return response;

            var authenticate = AuthenticateUser(logIn, user);

            if (authenticate)
            {
                var token = GenerateJSONWebToken(user);

                response = new LogInResponseDto
                {
                    FullName = user.FullName,
                    Token = token
                };
            }

            return response;
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto)
        {
            User user = new User();
            user.Id = Guid.NewGuid();
            user = _mapper.Map<User>(registerDto);

            UtilityHelper.CreatePasswordHash(registerDto.Password, out byte[] passHash, out byte[] passSalt);

            user.PasswordHash = passHash;
            user.PasswordSalt = passSalt;

            var addedUser = await _userRepository.AddAsync(user);
            await _unitOfWork.SaveAsync();

            return addedUser;
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationHelper.TokenSecret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, userInfo.UserName));

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                SigningCredentials = credentials,
                Expires = DateTime.UtcNow.AddMinutes(40),
                NotBefore = DateTime.UtcNow,
                Subject = new ClaimsIdentity(claims)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            string jasonToken = tokenHandler.WriteToken(token);
            return jasonToken;
        }

        private bool AuthenticateUser(LogInDto login, User user)
        {
            if (login == null || user == null)
                return false;

            if (VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
                return true;

            return false;
        }

        private bool VerifyPasswordHash(string password, byte[] userPassword, byte[] userSalt)
        {
            var hmac = new HMACSHA512(userSalt);
            byte[] passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));

            for (int i = 0; i < userPassword.Length; i++)
            {
                if (passwordHash[i] != userPassword[i])
                    return false;
            }
            return true;
        }

        public Guid GetUSerIDFromUserClaims(IEnumerable<Claim> claims) =>
            Guid.Parse(claims
                .FirstOrDefault(p => p.Type.Equals(ClaimTypes.NameIdentifier))
                .Value);
    }
}