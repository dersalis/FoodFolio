using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Entities;
using FoodFolio.WebApi.Exceptions;
using FoodFolio.WebApi.Helpers;
using FoodFolio.WebApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FoodFolio.WebApi.Services;

public class AccountService : IAccountService
{
    private readonly FoodFolioDbContext _dbContext;
    private readonly IPasswordHasher<User> _passordHasher;
    private readonly AuthenticationSettings _authenticationSettings;

    public AccountService(
        FoodFolioDbContext dbContext,
        IPasswordHasher<User> passordHasher,
        AuthenticationSettings authenticationSettings
    )
    {
        _dbContext = dbContext;
        _passordHasher = passordHasher;
        _authenticationSettings = authenticationSettings;
    }

    public async Task RegisterUserAsync(RegisterUserDto registerUser)
    {
        await UserHelper.CheckUserExistAsync(_dbContext, registerUser.Email);
        Role role = await RoleHelper.GetRoleByNameAsync(_dbContext, "User");

        User newUser = new()
        {
            Email = registerUser.Email,
            Role = role,
        };

        newUser.PasswordHash = _passordHasher.HashPassword(newUser, registerUser.Password);
        newUser.LastPasswordHash = newUser.PasswordHash;

        await _dbContext.Users.AddAsync(newUser);
        await _dbContext.SaveChangesAsync();
    }

    public string GenerateJwt(LoginDto login)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Email == login.Email);
        if (user is null)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var hasherResult = _passordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
        if (hasherResult == PasswordVerificationResult.Failed)
        {
            throw new BadRequestException("Invalid username or password");
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, $"{user.FirstName} {user.LastName}"),
            new Claim(ClaimTypes.Role, $"{user.Role.Name}"),
        };

        //if (!string.IsNullOrEmpty(user.Nationality))
        //{
        //    claims.Add(new Claim("Nationality", user.Nationality));
        //}

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddDays(_authenticationSettings.JwtExpireDays);

        var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
            _authenticationSettings.JwtIssuer,
            expires: expires,
            signingCredentials: cred,
            claims: claims);

        var tokenHandler = new JwtSecurityTokenHandler();

        return tokenHandler.WriteToken(token);
    }


    //private async Task<Role> GetRoleByNameAsync(string roleName)
    //{
    //    Role role = await _dbContext.Roles
    //        .FirstOrDefaultAsync(d => d.Name == roleName);

    //    if (role is null) throw new NotFoundException($"Role (name = {roleName}) not found");

    //    return role;
    //}

    //private async Task CheckUserExistAsync(string email)
    //{
    //    User user = await _dbContext.Users
    //        .FirstOrDefaultAsync(d => d.Email == email);

    //    if (user is not null) throw new NotFoundException($"User (email = {email}) exist");
    //}
}

