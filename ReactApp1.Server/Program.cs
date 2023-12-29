using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ReactApp1.Server.Data;
using ReactApp1.Server.Interfaces;
using ReactApp1.Server.Mappers;
using ReactApp1.Server.Models.Common;
using ReactApp1.Server.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyPolicy", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
        policy.SetIsOriginAllowed((host) => true);
        
    });
});

//Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
//Cookie
/*
.AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromSeconds(100);
    options.Cookie.Name = "JWTCookie";
    options.AccessDeniedPath = "/AccessDenied";
    options.SlidingExpiration = true;

})
*/
//JWT

.AddJwtBearer(options =>
{
    
    options.SaveToken = true;
    options.RequireHttpsMetadata = false; //going to be true
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
    options.Events = new JwtBearerEvents()
    {
        OnAuthenticationFailed = async (context) =>
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor= ConsoleColor.White;
            Console.WriteLine(context.Result);
            Console.ResetColor();
            //throw new Exception("Auth Failed"); // remove
        },
        OnTokenValidated = async (context) =>
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.BackgroundColor= ConsoleColor.White;
            Console.WriteLine(context.Result);
            Console.ResetColor();
        }

    };


});

//Idenitity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = true;

}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(Program));


var s = builder.Configuration["JWT:Secret"];



//Services
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddTransient<IJWTService, JWTService>();


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

app.UseCors("MyPolicy");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.MapFallbackToFile("/index.html");

app.Run();
