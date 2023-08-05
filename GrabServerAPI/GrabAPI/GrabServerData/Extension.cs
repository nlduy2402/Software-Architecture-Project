﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrabServerData
{
    public static class Extension
    {
        public static IServiceCollection AddUnitOfWork(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
        {
            services.AddDbContext<GrabDataContext>(optionsAction);
            services.AddScoped<UnitOfWork>();
            return services;

        }

        public static IServiceCollection AddServicesData(this IServiceCollection services)
        {
            return services;
        }
    }
}
