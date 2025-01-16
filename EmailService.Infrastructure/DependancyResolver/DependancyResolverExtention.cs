using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailService.Infrastructure.DependancyResolver
{
    public static class DependancyResolverExtention
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
