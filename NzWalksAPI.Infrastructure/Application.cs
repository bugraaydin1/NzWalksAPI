using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NzWalksAPI.Core.Middlewares;
using NzWalksAPI.Data;
using NzWalksAPI.Mappings;
using NzWalksAPI.Repositories.Repositories;

namespace NzWalksAPI.Infrastructure;

public static class Application
{
    public static WebApplicationBuilder AddNzWalksServices(this WebApplicationBuilder builder)
    {
        // Serve SPA frontend
        builder.Services.AddControllersWithViews();
        builder.Services.AddSpaStaticFiles(c =>
        {
            c.RootPath = "frontend/dist";
        });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(o =>
        {
            o.SwaggerDoc("v1", new OpenApiInfo { Version = "v1", Title = "NZ Walks API", });
            o.SwaggerDoc("v2", new OpenApiInfo { Version = "v2", Title = "NZ Walks API v2", });
            o.AddSecurityDefinition(
               JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
               {
                   Name = "Authorization",
                   In = ParameterLocation.Header,
                   Type = SecuritySchemeType.ApiKey,
                   Scheme = JwtBearerDefaults.AuthenticationScheme,
               });
            o.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
        {
            new OpenApiSecurityScheme{
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id= JwtBearerDefaults.AuthenticationScheme,
                },
                Scheme= "Oauth2",
                Name= JwtBearerDefaults.AuthenticationScheme,
                In= ParameterLocation.Header,
            },
            new List<string>()
        }
            });
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllers();

        // API versioning & swagger API versioning
        builder.Services.AddApiVersioning(c =>
        {
            c.DefaultApiVersion = new ApiVersion(1, 0);
            c.ReportApiVersions = true;
            c.AssumeDefaultVersionWhenUnspecified = true;
            c.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(), new HeaderApiVersionReader("x-api-version"), new MediaTypeApiVersionReader("x-api-version"));
        });
        builder.Services.AddVersionedApiExplorer(setup =>
        {
            setup.GroupNameFormat = "'v'VVV";
            setup.SubstituteApiVersionInUrl = true;
        });

        // DB context
        builder.Services.AddDbContext<NZWalksDbContext>(o =>
        {
            o.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString"));
        });
        builder.Services.AddDbContext<NZWalksAuthDbContext>(o =>
        {
            o.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksAuthConnectionString"));
        });

        // dependency injection
        builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();
        builder.Services.AddScoped<IWalkRepository, SQLWalkRepository>();
        builder.Services.AddScoped<ITokenRepository, TokenRepository>();
        builder.Services.AddScoped<IImageRepository, LocalImageRepository>();

        // mapping
        builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

        // identity configuration
        builder.Services.AddIdentityCore<IdentityUser>()
            .AddRoles<IdentityRole>()
            .AddTokenProvider<DataProtectorTokenProvider<IdentityUser>>("NZWalks")
            .AddEntityFrameworkStores<NZWalksAuthDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(o =>
        {
            o.Password.RequireDigit = true;
            o.Password.RequireNonAlphanumeric = false;
            o.Password.RequireLowercase = false;
            o.Password.RequireUppercase = false;
            o.Password.RequiredLength = 6;
            o.Password.RequiredUniqueChars = 3;
        });

        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(o => o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            });

        return builder;
    }

    public static WebApplication UseNzWalksMiddleware(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        return app;
    }
}



