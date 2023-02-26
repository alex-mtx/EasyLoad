using Common.CosmosDbServices;
using EasyLoad.Truckers.Ioc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Reflection;
using TruckersApi;
using TruckersApi.Filters;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", false, true)
    .AddEnvironmentVariables()
    .AddUserSecrets<Program>();

builder.Services.AddOptions<CosmosDbOptions>().BindConfiguration(CosmosDbOptions.CosmosDb);

builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddTruckersDomainServices(builder.Configuration);
builder.Services.InitializeCosmosCosmosDbService(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddMicrosoftIdentityWebApi(options =>
              {
                  builder.Configuration.Bind("AzureAd", options);
                  options.Events = new JwtBearerEvents();
                  options.Events.OnTokenValidated = async context =>
                  {
                      string[] allowedClientApps = builder.Configuration.GetSection("AzureAd:AllowedConsumers").Get<string[]>();
                      string? clientappId = context?.Principal?.Claims
                          .FirstOrDefault(x => x.Type == "azp" || x.Type == "appid")?.Value;

                      if (!allowedClientApps.Contains(clientappId))
                      {
                          throw new UnauthorizedAccessException("The client app is not permitted to access this API");
                      }

                      await Task.CompletedTask;
                  };

              }, options =>
              {
                  builder.Configuration.Bind("AzureAd", options);
              });


// The following flag can be used to get more descriptive errors in development environments
// Enable diagnostic logging to help with troubleshooting.  For more details, see https://aka.ms/IdentityModel/PII.
// You might not want to keep this following flag on for production
IdentityModelEventSource.ShowPII = false;

builder.Services.AddMvc(options =>
{
    options.Filters.Add<CustomExceptionFilter>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    var securityScheme = new OpenApiSecurityScheme()
    {
        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
    };
    var securityRequirement = new OpenApiSecurityRequirement
{
    {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearerAuth"
                }
            },
            new string[] {}
    }
};
    options.AddSecurityDefinition("bearerAuth", securityScheme);
    options.AddSecurityRequirement(securityRequirement);
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Truckers API",
        Description = "Truckers are one of the three pillars of Easy Load. This API offers methods to query and to manage their lifecycle.",
        TermsOfService = new Uri("https://termify.io/terms-of-service-generator"),
        Contact = new OpenApiContact
        {
            Name = "Email",
            Email = "contact@dummy.easy-load.com"
        },
        License = new OpenApiLicense
        {
            Name = "License",
            Url = new Uri("https://termify.io/eula-generator")
        }
    });

    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())

{
    IdentityModelEventSource.ShowPII = true;
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
