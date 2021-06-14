using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using ToDo.Core;
using ToDo.Core.Services;
using ToDo.Repositories;
using ToDo.Service;
using ToDoService;

namespace ToDo.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ContextDB>(options => 
            //options.UseInMemoryDatabase(databaseName: "ToDo")
            //        .EnableSensitiveDataLogging()
            //    //  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
            //    );

            services.AddDbContext<ContextDB>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAutoMapper(typeof(Startup));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<ContextDB>()
                .AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                var url = Configuration["frontend_url"];
                options.AddDefaultPolicy(builder => builder.WithOrigins(url).AllowAnyMethod().AllowAnyHeader());
            });           

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(Configuration["JWT_SECRET_KEY"])),
                    ClockSkew = TimeSpan.Zero
                });

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

            services.AddSwaggerGen(c =>
             {
                 c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo.API", Version = "v1" });
             });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IToDoService, ToDoService.ToDoService>();
            services.AddTransient<IFolderService, FolderService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo.API v1"));
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //var context = app.ApplicationServices.GetService<ContextDB>();
            //AddTestData(context);
        }

        private void AddTestData(ContextDB context)
        {
            var rootFolder = new Entities.Folder
            {
                Id = 1,
                Name = "Root",
                Enabled = true
            };

            context.Folders.Add(rootFolder);

            context.SaveChanges();
        }



    }
}
