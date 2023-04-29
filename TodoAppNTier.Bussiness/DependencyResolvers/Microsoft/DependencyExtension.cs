using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtier.DataAccess.Context;

namespace TodoAppNTier.Bussiness.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt => opt.UseSqlServer("Server=DESKTOP-S2C7UGO;Database=TodoDBUdemy;User ID=sa;Password=arkadas1"));
        }
    }
}
