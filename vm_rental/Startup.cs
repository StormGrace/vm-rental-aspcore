﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vm_rental.Data;
using vm_rental.Data.Interface;
using vm_rental.Data.Model;
using vm_rental.Data.Repository;
using Westwind.AspNetCore.LiveReload;
using Microsoft.EntityFrameworkCore.InMemory;
using vm_rental.Models.Services;
using FluentValidation.AspNetCore;
using vm_rental.ViewModels.Account;
 
namespace vm_rental
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLiveReload();
            services.AddTransient<FluentValidation.IValidator<ClientViewModel>, ClientValidator>();

            services.ConfigureMySqlContext(Configuration);

            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientHistoryRepository, ClientHistoryRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserHistoryRepository, UserHistoryRepository>();

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                    .AddFluentValidation(fv =>
                    {
                        fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
                    }); 

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseLiveReload();

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

    

            app.UseStaticFiles();
            app.UseHttpsRedirection();
 
            app.UseMvc(routes =>
            {
        
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "SignUp",
                    template: "{controller=Account}/{action=SignUp}/{id?}");

                routes.MapRoute(
                  name: "SignIn",
                  template: "{controller=Account}/{action=SignIn}/{id?}");
            });

            DbInitializer.Seed(app);

        }
    }
}
