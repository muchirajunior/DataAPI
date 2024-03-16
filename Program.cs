using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using DataAPI;
using DataAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration=builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>(options=>
options.UseSqlite(Configuration.GetConnectionString("DatabaseConnection")));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddResponseCaching();

builder.Services.AddSession(option=>{
    option.IdleTimeout=TimeSpan.FromHours(2);
    option.Cookie.Name = "DataAPI";
});

builder.Services.AddCors(option=>option.AddDefaultPolicy(
    policy=>policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options=>{
        options.TokenValidationParameters = new TokenValidationParameters(){
            ValidateIssuer = false, //set it to false if you want often change dormain names    
            ValidateAudience = true,    
            ValidateLifetime = true,    
            ValidateIssuerSigningKey = true,    
            ValidIssuer = Configuration["JwtSettings:validIssuer"],    
            ValidAudience = Configuration["JwtSettings:ValidAudience"],    
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SignKey"]!))
        };
    options.Events = new JwtBearerEvents{
        OnTokenValidated = async context =>{
            DatabaseContext _context = new DatabaseContext();
            var email = context.Principal!.FindFirstValue(ClaimTypes.Email);
            var securityStamp = context.Principal!.FindFirstValue(ClaimTypes.SerialNumber); // use the claim for security stamp
            var user = await _context.Users.Where((user)=>user.Email == email).FirstOrDefaultAsync();
            if(user== null || (user.SecurityStamp != securityStamp )){
                context.Fail("Inavild Security Stamp");
            }
            
        }
    };
});

builder.Services.AddScoped<IUserService,UserServices>();
// builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddControllers().AddJsonOptions(x =>x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseCors();

app.UseExceptionHandler("/error");

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.UseSession();

app.UseResponseCaching();

app.MapControllers();

// app.UseJwtValidation();

app.Run();
