using Autofac;
using Autofac.Extensions.DependencyInjection;
using RecruitTask.Controllers;
using RecruitTask.Models.Helpers;
using RecruitTask.Models.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using RecruitTask.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using RecruitTask.Models.Entities;
using FluentValidation.AspNetCore;
using RecruitTask.Models.Repositories;
using RecruitTask.Models.Entities.dbo;

namespace RecruitTask
{
    public class Startup
    {
        private const string connectionString= "Server=localhost;Database=RecruitTaskDatabase;Trusted_Connection=True;";
        public IConfiguration Configuration { get; }
        public ILifetimeScope AutofacContainer { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();

            services.AddMvc()
                  .AddControllersAsServices()
                  .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                  .AddNewtonsoftJson()
                  .AddFluentValidation(o => o.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            var sessionFactory = SessionFactoryBuilder.BuildSessionFactory(connectionString, false, true);
            services.AddSingleton(sessionFactory);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<UsersController>().PropertiesAutowired();
            builder.RegisterType<CompanyController>().PropertiesAutowired();

            builder.Register(c => c.Resolve<ISessionFactory>().OpenSession()).As<ISession>();
            builder.Register(c => c.Resolve<ICompanyRepository>()).InstancePerLifetimeScope();
            builder.Register(context => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CompanyRequest, Company>();
                cfg.CreateMap<EmployerRequest, Employer>();
                cfg.CreateMap<Company, CompanyRequest>();
                cfg.CreateMap<Employer, EmployerRequest>();
                cfg.CreateMap<CompanyFilterRequest, CompanyFilter>();

            })).AsSelf().SingleInstance();

            builder.Register(c =>
            {
                var context = c.Resolve<IComponentContext>();
                var config = context.Resolve<MapperConfiguration>();
                return config.CreateMapper(context.Resolve);
            })
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();

            app.UseRouting();
            app.UseAuthorization();
            app.UseMiddleware<JwtMiddleware>();
        }
    }
}
