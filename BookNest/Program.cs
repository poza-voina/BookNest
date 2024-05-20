using System.Text;
using Infrastructure;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", b =>
    {
        b
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = Constants.Authentication.Issuer,
            ValidateAudience = true,
            ValidAudience = Constants.Authentication.Audience,
            ValidateLifetime = true,
            LifetimeValidator = (DateTime? notBefore, DateTime? expires, SecurityToken securityToken, 
                TokenValidationParameters validationParameters) =>
            {
                if (expires.HasValue && expires.Value < DateTime.UtcNow)
                {
                    return false; 
                }

                if (notBefore.HasValue && notBefore.Value > DateTime.UtcNow)
                {
                    return false;
                }

                return true;
            },
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Authentication.SecretKey)),
            ValidateIssuerSigningKey = true
        };
    });

services.AddAuthorization();

var connectionString =
    "User ID=postgres;Password=psql;Server=localhost;Port=5432;Database=BookNest;Include Error Detail=true";
services.AddDbContext<ApplicationDbContext>(options=> options.UseNpgsql(connectionString));


services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddSingleton<IPasswordManager, PasswordManager>();
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IAuthorRepository, AuthorRepository>();
services.AddScoped<IBookRepository, BookRepository>();
services.AddScoped<IReviewRepository, ReviewRepository>();



var app = builder.Build();


app.UseCors("CORSPolicy");
app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.Run();

