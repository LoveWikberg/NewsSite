using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NewsSite.Data;
using NewsSite.CustomRequirements;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace NewsSite
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();

            services.AddTransient<DataHandler>();

            services.AddSingleton<IAuthorizationHandler, PublishRightsHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("SportsNews", p => p.AddRequirements(new PublishRightsRequirements("Sports")));
                options.AddPolicy("CultureNews", p => p.AddRequirements(new PublishRightsRequirements("Culture")));
                options.AddPolicy("HiddenNews", p => p.RequireRole("Administrator", "Publisher", "Subscriber"));
                options.AddPolicy("AgeLimit", p => p.RequireAssertion(context =>
                {
                    if (context.User.HasClaim(c => c.Type == "Age"))
                    {
                        int age = int.Parse(context.User.Claims.SingleOrDefault(c => c.Type == "Age").Value);
                        if (age >= 20)
                            return true;
                    }
                    return false;
                }
                ));
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}

