using Microsoft.OpenApi.Models;
using System.Reflection;
using MongoDB.Driver;
using DataAccessObject;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Repository.Implementation;
using JwtTokenAuthorization;
using Services;
using Services.Implementation;
using Middleware;
using Utils.PasswordHasher;
using Utils.RandomGenerator;
using Repository.Interface;
using Repository;
using WebAPI.MappingProfile;
using Utils.ImageUtil;
using Utils.ImageProcessing;
using AzureBlobStorage;

namespace WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAutoMapper(typeof(ProfileMapper));
            builder.Services.AddDbContext<SqlDbContext>(s =>
            {
                string connectionString = builder.Configuration
                    .GetConnectionString("SqlConnectionString")
                    ?? throw new Exception("Can not get connection string");
                s.UseSqlServer(connectionString, option => { option.EnableRetryOnFailure(); });
            });
            builder.Services.AddSingleton<IMongoClient>(s =>
            {
                string connectionString = builder.Configuration
                    .GetConnectionString("MongoDbConnectionString")
                    ?? throw new Exception("Can not get connection string");
                return new MongoClient(connectionString);
            });
            builder.Services.AddScoped<ITokenHelper, JwtTokenHelper>();
            builder.Services.AddScoped<IDaoFactory,DaoFactory>();

            builder.Services.AddScoped<IArtInfoRepository, ArtInfoRepository>();    
            builder.Services.AddScoped<IArtRatingRepository, ArtRatingRepository>();
            builder.Services.AddScoped<ICommissionRepository, CommissionRepository>();
            builder.Services.AddScoped<ICreatorInfoRepository, CreatorInfoRepository>();
            builder.Services.AddScoped<IFollowRepository, FollowRepository>();
            builder.Services.AddScoped<IImageInfoRepository, ImageInfoRepository>();
            builder.Services.AddScoped<IPostContentRepository, PostContentRepository>();
            builder.Services.AddScoped<IPostRepository, PostRepository>();
            builder.Services.AddScoped<IPostLikeRepository, PostLikeRepository>();
            builder.Services.AddScoped<IPurchaseRepository, PurchaseRepository>();
            builder.Services.AddScoped<ISystemRevenueRepository, SystemRevenueRepository>();
            builder.Services.AddScoped<ITagRepository, TagRepository>();
            builder.Services.AddScoped<IArtTagRepository,ArtTagRepository>();

            builder.Services.AddScoped<IArtService,ArtService>();
            builder.Services.AddScoped<IArtRatingService, ArtRatingService>();
            builder.Services.AddScoped<ICreatorInfoService, CreatorInfoService>();
            builder.Services.AddScoped<ITagService, TagService>();
            builder.Services.AddScoped<ITransactionHistoryService, TransactionHistoryService>();
            builder.Services.AddScoped<IReportRepository,ReportRepository>();
            builder.Services.AddScoped<ITransactionHistoryRepository , TransactionHistoryRepository>();
            builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
            builder.Services.AddScoped<IPurchaseService , PurchaseService>();

            builder.Services.AddScoped<IAuthenticationService,AuthenticationService>();
            builder.Services.AddScoped<ICommissionService, CommissionService>();
            builder.Services.AddScoped<IUserInfoService, UserInfoService>();
            builder.Services.AddScoped<IPostService, PostService>();
            builder.Services.AddScoped<IReportService, ReportService>();
            builder.Services.AddTransient<ISendEmail, SendEmail>();

            builder.Services.AddScoped<IAzureBlobStorage, AzureBlobStorage.AzureBlobStorage>();
            builder.Services.AddScoped<IImageUtil, ImageUtil>();
            builder.Services.AddScoped<IPasswordHasher,PasswordHasher>();
            builder.Services.AddScoped<ITokenHelper,JwtTokenHelper>();
            builder.Services.AddScoped<IRandomGenerator, RandomGenerator>();
            

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                string issuer = builder.Configuration["Jwt:Issuer"]
                    ?? throw new Exception("Can not find jwt issuer in config file");
                string audience = builder.Configuration["Jwt:Audience"]
                    ?? throw new Exception("Can not find jwt audience in config file");
                string secretKey = builder.Configuration["Jwt:SecretKey"]
                    ?? throw new Exception("Can not find jwt secret key in config file");
                o.TokenValidationParameters = new()
                {
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey
                    (Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });
            builder.Services.AddAuthorization();
            builder.Services.AddCors(option =>
            {
                option.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                });
            });
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(config =>
            {
                ConfigureSwagger(config);
            });


            var app = builder.Build();

            app.UseStaticFiles();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.EnableTryItOutByDefault();
                    c.InjectStylesheet("/swagger-ui-css/SwaggerDark.css");
                });
            }
            else
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Digital Art Trading Platform API");
                    c.EnableTryItOutByDefault();
                    c.InjectStylesheet("/swagger-ui-css/SwaggerDark.css");
                    c.RoutePrefix = "";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.UseMiddleware<ErrorHandlingMiddleware>();
            app.Run();
        }
        private static void ConfigureSwagger(SwaggerGenOptions swaggerGenOptions)
        {
            swaggerGenOptions.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Api",
                        Description = "Api Description",
                        Version = "v1",
                    });
            swaggerGenOptions.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please Enter Token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "bearer"
            });
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        Array.Empty<string>()
                    }
                });
        }
    }
}
