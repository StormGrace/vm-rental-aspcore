﻿using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;
using Westwind.AspNetCore.LiveReload;
using vm_rental.Data;
using vm_rental.Data.JSON;
using vm_rental.Data.Model;
using vm_rental.Models.Identity;
using vm_rental.ServiceExtensions;
using Microsoft.AspNetCore.Mvc.Razor;

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
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddLiveReload();
      services.AddHttpContextAccessor();

      services.ConfigureMySQL(Configuration);
      services.ConfigureEmail(Configuration);
      services.ConfigureJWT();

      services.ConfigureFluentValidators();

      services.ConfigureRepositoryDI();
      services.ConfigureIdentityDI(Configuration);


      services.Configure<CookiePolicyOptions>(options =>
      {
        // This lambda determines whether user consent for non-essential cookies is needed for a given request.
        options.CheckConsentNeeded = context => true;
        options.MinimumSameSitePolicy = SameSiteMode.Strict;
      });

      services.AddAuthentication(options =>
      {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
      })
      .AddJwtBearer("Bearer", options =>
      {
        options.SaveToken = true;
        options.RequireHttpsMetadata = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
          IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(Configuration["jwt:SecretKey"])),
          ValidIssuer = Configuration["jwt:Iss"],
          ValidAudience = Configuration["jwt:Aud"],
          ValidateIssuerSigningKey = true,
          ValidateIssuer = true,
          ValidateAudience = true,
          ClockSkew = TimeSpan.Zero
        };
      });

      services.Configure<IdentityOptions>(options => {
        options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
        options.Lockout.MaxFailedAccessAttempts = 3;
        options.Lockout.AllowedForNewUsers = true;
      });

      services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

      services.ConfigureApplicationCookie(options =>
      {
        options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        options.Cookie.Name = "vm_rental";
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.LoginPath = "/Sign/Signin";
        options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
        options.SlidingExpiration = true;
      });

      services.AddIdentity<User, UserRole>(config =>
      {
        config.SignIn.RequireConfirmedEmail = true;

      }).AddEntityFrameworkStores<VmDbContext>()
        .AddUserManager<CustomUserManager>()
        .AddSignInManager<CustomSignInManager>()
        .AddUserStore<CustomUserStore>()
        .AddDefaultTokenProviders();

      services.AddMvc().AddFluentValidation(fv =>
      {
          fv.RunDefaultMvcValidationAfterFluentValidationExecutes = true;
          fv.RegisterValidatorsFromAssemblyContaining<Startup>();
      });

      services.Configure<RazorViewEngineOptions>(options =>
      {
        options.AreaViewLocationFormats.Clear();
        options.AreaViewLocationFormats.Add("Views/{2}/{1}/{0}.cshtml");
        options.AreaViewLocationFormats.Add("Views/{2}/Shared/{0}.cshtml");
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseDatabaseErrorPage();
        app.UseLiveReload();
      }
      else
      {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseCookiePolicy();
      app.UseAuthentication();

      app.UseMvc(routes =>
      {
          routes.MapAreaRoute(
           name: "default",
           areaName: "Website",
           template: "{controller=Home}/{action=Index}/{id?}");

          routes.MapRoute(
            name: "SignUp",
            template: "{area:exists}/{controller=Sign}/{action=SignUp}");

          routes.MapRoute(
            name: "SignIn",
            template: "{area:exists}/{controller=Sign}/{action=SignIn}");

          routes.MapRoute(
            name: "CPanel",
            template: "{area:exists}/{controller=Account}/{action=Account}");

      });

      JSONRepository.Initialize();
    }
  }
}
