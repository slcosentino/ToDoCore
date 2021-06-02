using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ToDo.Core;
using ToDo.Core.Services;
using ToDo.Repositories;
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
            services.AddDbContext<ContextDB>(options => 
            options.UseInMemoryDatabase(databaseName: "ToDo")
                    .EnableSensitiveDataLogging()
                //  .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                );

            
            //services.AddDbContext<ContextDB>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddAutoMapper(typeof(Startup));
            services.AddControllers();
           //services.AddControllers( options => 
           //{
           //    options.Filters.Add(typeof(FilterException));
           //});
           services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ToDo.API", Version = "v1" });
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IToDoService, ToDoService.ToDoService>();
            services.AddTransient<IFolderService, FolderService>(); 
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ToDo.API v1"));
            }
            

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var context = app.ApplicationServices.GetService<ContextDB>();
            AddTestData(context);
        }


    }
}
