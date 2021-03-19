using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using DGMLD3.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DGMLD3.Services;
using System.Security.Claims;
using DGMLD3.Utils;
using DGMLD3.Data.CONTEXT;
using DGMLD3.Data.RDMS;

namespace DGMLD3
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(
                    Secrets.GetConnectionString(Configuration, "DefaultConnectionDB"), options => options.SetPostgresVersion(13,2)));
            
            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = Secrets.GetConnectionString(Configuration, "ConnectionStringRedis");
            });

            services.AddSingleton<GraphRedisService, GraphRedisService>();

            services.AddControllersWithViews();
            services.AddRazorPages()
        .AddRazorRuntimeCompilation();

            services.AddResponseCaching();
            services.AddResponseCompression();
            services.AddTransient<UserManager<ApplicationUser>>();
            services.AddTransient<RoleManager<IdentityRole>>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseResponseCaching();
            app.UseResponseCompression();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }

        private async Task CreateUserRoles(IApplicationBuilder app)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            var RoleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //var JobsRepo = scope.ServiceProvider.GetRequiredService<IJobPostingRepository>();
            //await JobsRepo.BuildCache();
            var content = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            IdentityResult roleResult;

            //Adding Admin Role
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            //Assign Admin role to the main User here we have given our newly registered 
            //login id for Admin management
            // Also Assigning them Claims to perform CUD operations
            ApplicationUser user = await UserManager.FindByEmailAsync("avaneesab5@gmail.com");
            if (user != null)
            {
                var currentUserRoles = await UserManager.GetRolesAsync(user);
                if (!currentUserRoles.Contains("Admin"))
                {
                    await UserManager.AddToRoleAsync(user, "Admin");
                }

                //var currentClaims = await UserManager.GetClaimsAsync(user);
                //if (!currentClaims.Any())
                //{
                //    var CanCreatePostingClaim = new Claim("CanViewGraphs", "True");
                //    await UserManager.AddClaimAsync(user, CanCreatePostingClaim);

                //    var CanEditPostingClaim = new Claim("CanEditPosting", "True");
                //    await UserManager.AddClaimAsync(user, CanEditPostingClaim);

                //    var CanDeletePostingClaim = new Claim("CanDeletePosting", "True");
                //    await UserManager.AddClaimAsync(user, CanDeletePostingClaim);
                //}
            }

        }
    }
}
