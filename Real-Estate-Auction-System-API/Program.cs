using System.Text;
using System.Text.Json.Serialization;
using BusinessObjects.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Real_Estate_Auction_System_API.Hubs;
using Stripe;
using Utils;

namespace Real_Estate_Auction_System_API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        StripeConfiguration.ApiKey = builder.Configuration["StripeSecretKey"];
        
        builder.Services.AddSignalR();
        builder.Services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddScoped<IChatHub, ChatHub>();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        
        //Cái này để migrate db, khi nào gần tới present thì xóa
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "REASProject", Version = "v1" });
            options.SchemaFilter<EnumSchemaFilter>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
            };
            options.Events = new JwtBearerEvents()
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    context.Token = accessToken;
                    Console.WriteLine($"Received access_token: {context.Token}");
                    return Task.CompletedTask;
                },
                OnTokenValidated = context =>
                {   // Log information about the validated token
                    Console.WriteLine("Token validated successfully!");
                    return Task.CompletedTask;
                },
            };
        });
        builder.Services.AddCors(options
            => options.AddDefaultPolicy(policy
                => policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.MapHub<ChatHub>("chat-hub", options =>
        {
            options.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets;
        });
        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseCors();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}