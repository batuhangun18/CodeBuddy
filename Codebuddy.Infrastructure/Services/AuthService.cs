using Codebuddy.Application.DTOs.Auth;
using Codebuddy.Application.Interfaces;
using Codebuddy.Domain.Entities;
using Codebuddy.Infrastructure.Identity;
using Codebuddy.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Codebuddy.Infrastructure.Services;

public class AuthService : IAuthService
{
    private readonly CodebuddyDbContext _context;
    private readonly PasswordHasher _passwordHasher;
    private readonly JwtTokenGenerator _tokenGenerator;

    public AuthService(CodebuddyDbContext context, PasswordHasher passwordHasher, JwtTokenGenerator tokenGenerator)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await _context.Users.AnyAsync(u => u.Email == request.Email);
        if (existing)
        {
            throw new InvalidOperationException("Email already registered.");
        }

        var user = new User
        {
            Id = Guid.NewGuid(),
            Email = request.Email,
            UserName = request.UserName,
            PasswordHash = _passwordHasher.Hash(request.Password),
            CreatedAt = DateTime.UtcNow
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            SubscriptionType = user.SubscriptionType,
            Token = _tokenGenerator.Generate(user)
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
        if (user is null)
        {
            return null;
        }

        var isValid = _passwordHasher.Check(user.PasswordHash, request.Password);
        if (!isValid)
        {
            return null;
        }

        return new AuthResponse
        {
            UserId = user.Id,
            Email = user.Email,
            UserName = user.UserName,
            SubscriptionType = user.SubscriptionType,
            Token = _tokenGenerator.Generate(user)
        };
    }
}
