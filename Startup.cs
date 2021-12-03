using FluentValidation;
using FluentValidation.AspNetCore;
using Forum.Authorization;
using Forum.Entities;
using Forum.Middleware;
using Forum.Models;
using Forum.Models.Validators;
using Forum.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Forum
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
            services.AddDbContext<ForumDbContext>();

            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<Role>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>()
                .AddEntityFrameworkStores<ForumDbContext>();



            services.AddControllersWithViews(); 

            
            services.AddScoped<ErrorHandlingMiddleware>();
            services.AddScoped<ForumSeeder>();
            services.AddSwaggerGen();
            services.AddAutoMapper(this.GetType().Assembly);
            services.AddHttpContextAccessor();

            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<IResponseService, ResponseService>();
            services.AddScoped<IBlackListService, BlackListService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserContextService, UserContextService>();

            services.AddControllers().AddFluentValidation();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IAuthorizationHandler, TopicOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, ResponseOperationRequirementHandler>();
            services.AddScoped<IAuthorizationHandler, UserOperationRequirementHandler>();

            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ForumSeeder seeder, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            seeder.SeedData(userManager, roleManager);
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.UseRouting();
            app.UseAuthentication();
            //app.UseMvc();
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Forum API");
            });
            app.UseStaticFiles();

            

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Category}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
