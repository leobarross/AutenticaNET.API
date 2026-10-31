using AutenticaNET.API.Data;
using AutenticaNET.API.DTOs;
using AutenticaNET.API.Models;
using AutenticaNET.API.Services.interfaces;
using AutenticaNET.API.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AutenticaNET.API.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDbContext _db;
    private readonly JwtSettings _jwtSettings;

    public AuthService(ApplicationDbContext db, IOptions<JwtSettings> jwtSettings)
    {
        _db = db;
        _jwtSettings = jwtSettings.Value;
    }

    public Task<User> GetByIdAsync(string id) => _db.Users.FindAsync(id).AsTask();

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var email = dto.Email.Trim();
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == email);
        if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            return null;

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Username),
            new(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var token = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<UserDto> RegisterAsync(RegisterDto dto)
    {
        var email = dto.Email.Trim();
        var username = dto.Username.Trim();

        if (await _db.Users.AnyAsync(u => u.Email == email))
            throw new InvalidOperationException("Email já cadastrado");

        if (await _db.Users.AnyAsync(u => u.Username == username))
            throw new InvalidOperationException("Username já cadastrado");

        var user = new User(username, email, BCrypt.Net.BCrypt.HashPassword(dto.Password));
        _db.Users.Add(user);
        await _db.SaveChangesAsync();

        return new UserDto { Id = user.Id, Username = user.Username, Email = user.Email };
    }
}
