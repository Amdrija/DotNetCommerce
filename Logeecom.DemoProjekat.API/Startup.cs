using AutoMapper;
using Logeecom.DemoProjekat.BL.Services;
using Logeecom.DemoProjekat.DAL.Models;
using Logeecom.DemoProjekat.DAL.Repositories;
using Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels;
using Logeecom.DemoProjekat.PL.ViewModels.SubmitionModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using DALProductQuery = Logeecom.DemoProjekat.DAL.QueryModels.ProductQuery;
using DALCategoryQuery = Logeecom.DemoProjekat.DAL.QueryModels.CategoryQuery;
using PLProductQuery = Logeecom.DemoProjekat.PL.ViewModels.QueryModels.ProductQuery;
using PLCategoryQuery = Logeecom.DemoProjekat.PL.ViewModels.QueryModels.CategoryQuery;
using Logeecom.FileManager;
using Logeecom.DemoProjekat.DAL;
using System.Collections.Generic;
using System.Linq;

namespace demoProjekat
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DemoDbContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(Configuration.GetConnectionString("demoDb")));

            services.AddScoped<CategoryRepository, CategoryRepository>();
            services.AddScoped<CategoryService, CategoryService>();

            services.AddScoped<ProductRepository, ProductRepository>();
            services.AddScoped<ProductService, ProductService>();

            services.AddScoped<ImageService, ImageService>();

            services.AddScoped<UserRepository, UserRepository>();
            services.AddScoped<LoginService, LoginService>();

            services.AddScoped<UnitOfWork, UnitOfWork>();

            services.AddScoped<IFileManager>(x => new WebRootFileManager(this.Environment.WebRootPath));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategorySubmitModel, Category>();
                cfg.CreateMap<Category, CategoryResponseModel>();
                cfg.CreateMap<PLCategoryQuery,
                    DALCategoryQuery>();

                cfg.CreateMap<ProductSubmitionModel, Product>();
                cfg.CreateMap<Product, ProductResponseModel>();
                cfg.CreateMap<PLProductQuery,
                    DALProductQuery>();

            });
            services.AddScoped<IMapper, Mapper>(s => new Mapper(config));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = Configuration["Jwt:Issuer"],
                            ValidAudience = Configuration["Jwt:Audience"],
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                        };
                    });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo Projekat", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement 
                {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },
                        new string[] { }
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, DemoDbContext dbContext, LoginService loginService)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "demoProjekat v1"));
            }
            app.UseStaticFiles();

            app.UseExceptionHandler("/error");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            
            // If there  are unapplied migrations on startup,
            // this part of code applies these migrations
            if(dbContext.Database.GetPendingMigrations().Any())
            {
                dbContext.Database.Migrate();
                loginService.CreateAdmin(new User() { Username = "andrija", Password = "nikola123" });
            }

            
        }
    }
}
