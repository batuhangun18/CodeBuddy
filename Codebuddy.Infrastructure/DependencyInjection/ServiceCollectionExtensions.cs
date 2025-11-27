using Codebuddy.Application.Interfaces;
using Codebuddy.Infrastructure.Identity;
using Codebuddy.Infrastructure.Options;
using Codebuddy.Infrastructure.Persistence;
using Codebuddy.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Codebuddy.Infrastructure.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddDbContext<CodebuddyDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            options.UseSqlServer(connectionString);
        });

        services.AddScoped<PasswordHasher>();
        services.AddScoped<JwtTokenGenerator>();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IChallengeService, ChallengeService>();
        services.AddScoped<ISubmissionService, SubmissionService>();
        services.AddScoped<IBuddyService, BuddyService>();
        services.AddScoped<IDiscussionService, DiscussionService>();

        return services;
    }
}
