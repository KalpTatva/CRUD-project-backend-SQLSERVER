using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using crud.repository.Interfaces;
using crud.repository.Models;
using crud.repository.ViewModels;
using crud.service.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using static crud.repository.Helpers.Enums;

namespace crud.service.Implementations;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(IConfiguration config, IUserRepository userRepository, IRefreshTokenRepository refreshTokenRepository)
    {
        _config = config;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
    }

    public async Task<AuthResultViewModel> GenerateToken(LoginRequestViewModel req)
    {
        try
        {
            User? user = await _userRepository.GetUserByEmail(req.Email);
            string RoleName = ((RoleEnum)(user?.RoleId ?? 0)).ToString();

            Byte[] key = Encoding.UTF8.GetBytes(_config["Jwt:Key"] ?? "");
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, req.Email),
                    new Claim(ClaimTypes.NameIdentifier, user?.UserId.ToString() ?? ""),
                    new Claim(ClaimTypes.Role, RoleName)
                }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
            string? accessToken = tokenHandler.WriteToken(token);

            RefreshToken refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                JwtId = token.Id,
                IsUsed = false,
                IsRevoked = false,
                UserId = user?.UserId ?? 0,
                ExpiresAt = req.RememberMe ? DateTime.UtcNow.AddDays(30) : DateTime.UtcNow.AddDays(1)
            };

            await _refreshTokenRepository.AddRefreshToken(refreshToken);

            return new AuthResultViewModel
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                ExpiresIn = 1800
            };
        }
        catch (Exception e)
        {
            throw new Exception($"Error occured while generating token! , {e.Message} ");
        }
    }

}
