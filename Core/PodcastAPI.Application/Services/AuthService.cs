using AutoMapper;
using Microsoft.Extensions.Configuration;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.DTOs.Auth;
using PodcastAPI.Domain.Interfaces;
using PodcastAPI.Domain.Entities;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace PodcastAPI.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository, IMapper mapper, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _configuration = configuration;
        }
        public async Task<AuthResponse?> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
            if (user == null) return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash);
            if (!isPasswordValid) return null;

            var token = GenerateToken(user);

            return new AuthResponse
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Token = token
            };
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest)
        {
            var existingUser = _userRepository.GetByEmailAsync(registerRequest.Email).Result;
            if (existingUser != null)
                throw new Exception("User with this email already exists.");

            if (registerRequest.Password != registerRequest.ConfirmPassword)
                throw new Exception("Passwords do not match.");

            var newUser = _mapper.Map<User>(registerRequest);

            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            await _userRepository.AddAsync(newUser);

            var token = GenerateToken(newUser);

            return new AuthResponse
            {
                Id = newUser.Id,
                Username = newUser.Username,
                Email = newUser.Email,
                Token = token
            };
        }

        private string GenerateToken(User user)
        {
            var secretKey = _configuration["Jwt:Key"];
            var durationString = _configuration["Jwt:DurationMinutes"];

            if(string.IsNullOrEmpty(secretKey))
                throw new Exception("Jwt:Key value not found!");

            if (string.IsNullOrEmpty(durationString))
                throw new Exception("Jwt:DurationMinutes value not found!");

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // Subject with claims about the user
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(durationString)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
