using EmailService.Application.IServices;
using EmailService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EmailService.Application.DependancyResolver
{
    public static class DependancyResolverExtention
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailService, EmailService.Application.Services.EmailService>();
            services.AddTransient<IUserService, UserService>();
        }
    }
}
