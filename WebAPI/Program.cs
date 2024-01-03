using Microsoft.OpenApi.Models;
using System.Reflection;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
                config.SwaggerDoc("v1",
                    new OpenApiInfo
                {
                    Title = "Api",
                    Description = "Api Description",
                    Version = "v1",
                });
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);
                config.IncludeXmlComments(filePath);
                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please Enter Token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                config.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                }) ;
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "FA Intership Management API");
                    c.EnableTryItOutByDefault();
                    c.InjectStylesheet("/swagger-ui-css/SwaggerDark.css");
                    c.RoutePrefix = "";
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
