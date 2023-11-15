using Granp.Data;
using Granp.Services.Repositories.Interfaces;
using Granp.Services.Repositories;
using Granp.Middlewares;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configure Kesrel
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.AddServerHeader = false;
});

// Add SignalR
builder.Services.AddSignalR();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        // Allow any origin for testing purposes.
        policy.WithOrigins(
            "capacitor://localhost",
            "http://localhost"
        )
            .WithHeaders(new string[] {
                HeaderNames.ContentType,
                HeaderNames.Authorization,
            })
            .WithMethods("GET")
            .SetPreflightMaxAge(TimeSpan.FromSeconds(86400));
    });
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(options =>
    {
        options.Authority = builder.Configuration["Authentication:Domain"];
        options.Audience = builder.Configuration["Authentication:Audience"];
        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = ClaimTypes.NameIdentifier,
            RoleClaimType = ClaimTypes.Role
        };
    });


// Add EF DB Context
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add UnitOfWork to DI
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();

// Add Controllers
builder.Services.AddControllers();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
   option.SwaggerDoc("v1",
           new OpenApiInfo
           {
               Title = "API",
               Version = "v1",
               Description = "A REST API",
               TermsOfService = new Uri("https://lmgtfy.com/?q=i+like+pie")
           });

   option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
   {
       Name = "Authorization",
       In = ParameterLocation.Header,
       Type = SecuritySchemeType.OAuth2,
       Flows = new OpenApiOAuthFlows
       {
           Implicit = new OpenApiOAuthFlow
           {
               Scopes = new Dictionary<string, string>
               {
                    { "openid", "OpenID" },
                    { "profile", "Profile" },
                    { "email", "Email" },
                    { "offline_access", "Offline Access" }
               },
               AuthorizationUrl = new Uri(builder.Configuration["Authentication:Domain"] + "authorize?audience=" + builder.Configuration["Authentication:Audience"])
           }
       }
   });

   option.AddSecurityRequirement(new OpenApiSecurityRequirement
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


var app = builder.Build();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
        c.OAuthClientId(builder.Configuration["Authentication:ClientId"]);
        c.OAuth2RedirectUrl("http://localhost:5255/swagger/oauth2-redirect.html");
    });
}

// app.UseErrorHandler();
app.UseSecureHeaders();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
