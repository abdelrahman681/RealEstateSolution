using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using RealEstate.API.Error;
using RealEstate.Domain.Entiry.IdentityEntity;
using RealEstate.Domain.InterFace.Repository;
using RealEstate.Domain.InterFace.Services;
using RealEstate.Reopsitory.Context;
using RealEstate.Reopsitory.Repossitory;
using RealEstate.Services;
using System.Reflection;
using System.Text;

namespace RealEstate.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            // Add services to the container.
            #region Services
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
                     new string[]{}
        }
    });
            });

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost",
                "http://localhost:4200",
                "https://localhost:5000",
                "http://localhost:5001")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .SetIsOriginAllowedToAllowWildcardSubdomains();
                    });
            });
            builder.Services.AddDbContext<EstateContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
            });


            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<EstateContext>();

            builder.Services.AddIdentityCore<ApplicationUser>().AddEntityFrameworkStores<EstateContext>()
                .AddSignInManager<SignInManager<ApplicationUser>>();
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["Token:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["Token:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:Key"])),
                    ValidateLifetime = true,
                };
            });
            builder.Services.AddScoped<IPropertyServices, PropertyServices>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.Configure<ApiBehaviorOptions>(option =>
            {
                option.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState.Where(m => m.Value!.Errors.Any())
                    .SelectMany(m => m.Value!.Errors).Select(e => e.ErrorMessage).ToList();

                    return new BadRequestObjectResult(new APIValidationErrorResponse() { Errors = errors });
                };
            });
            #endregion

            var app = builder.Build();
            await InitialzeDb(app);

            #region Pipeline

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();    
                app.UseMiddleware<CoustomExceptionHandler>();
            }

            app.UseRouting();
            app.UseHttpsRedirection();
            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();
            app.Run();
            #endregion
        }








        //Create Private Method for create database if does nor exiset
        private static async Task InitialzeDb(WebApplication app)
        {
            using (var scope = app.Services.CreateAsyncScope())
            {
                var service = scope.ServiceProvider;
                var loggerFactor = service.GetRequiredService<ILoggerFactory>();
                try
                {
                    var context = service.GetRequiredService<EstateContext>();
                    var usermanger = service.GetRequiredService<UserManager<ApplicationUser>>();
                    if ((await context.Database.GetPendingMigrationsAsync()).Any())
                        await context.Database.MigrateAsync();
                    //Applay Seeding
                    await EstateContextSeeding.SeedData(context);
                    await EstateIdentitySeeding.SeedUsersAsync(usermanger);
                }
                catch (Exception ex)
                {

                    var logger = loggerFactor.CreateLogger<Program>();
                    logger.LogError(ex.Message);
                }
            }
        }
    }
}
