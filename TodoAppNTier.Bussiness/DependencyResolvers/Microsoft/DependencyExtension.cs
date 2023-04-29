using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoAppNtier.DataAccess.Context;
using TodoAppNtier.DataAccess.UnitOfWork;
using TodoAppNTier.Bussiness.Interfaces;
using TodoAppNTier.Bussiness.Mappings.AutoMapper;
using TodoAppNTier.Bussiness.Services;
using TodoAppNTier.Bussiness.ValidationRules;
using TodoAppNTier.Dtos.WorkDtos;

namespace TodoAppNTier.Bussiness.DependencyResolvers.Microsoft
{
    public static class DependencyExtension
    {
        public static void AddDependencies(this IServiceCollection services)
        {
            services.AddDbContext<TodoContext>(opt =>
            {
                opt.UseSqlServer("Server=DESKTOP-S2C7UGO;Database=TodoDBUdemy;User ID=sa;Password=arkadas1");
                opt.LogTo(Console.WriteLine, LogLevel.Information);
            });

            var configuration = new MapperConfiguration(opt =>
            {
                opt.AddProfile(new WorkProfile());
            });

            var mapper = configuration.CreateMapper();

            services.AddSingleton(mapper);

            services.AddScoped<IUow, Uow>();
            services.AddScoped<IWorkService, WorkService>();

            services.AddTransient<IValidator<WorkCreateDto>,WorkCreateDtoValidator>();
            services.AddTransient<IValidator<WorkUpdateDto>,WorkUpdateDtoValidator>();
        }
    }
}
