using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using DataAPI;
using DataAPI.Services;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

IConfiguration Configuration=builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DatabaseContext>();

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
            ValidateIssuer = false,   
            ValidateAudience = true,    
            ValidateLifetime = true,    
            ValidateIssuerSigningKey = true,    
            ValidIssuer = Configuration["JwtSettings:validIssuer"],    
            ValidAudience = Configuration["JwtSettings:ValidAudience"],    
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtSettings:SignKey"]!))
        };
    options.Events = new JwtBearerEvents{
        OnTokenValidated = async context =>{
            DatabaseContext _context = new DatabaseContext(configuration: Configuration!);
            var email = context.Principal!.FindFirstValue(ClaimTypes.Email);
            var securityStamp = context.Principal!.FindFirstValue(ClaimTypes.SerialNumber); // use the claim for security stamp
            var user = await _context.Users.Where((user)=>user.Email == email).FirstOrDefaultAsync();
            if(user== null || (user.SecurityStamp != securityStamp )){
                context.Fail("Invaild Security Stamp");
            }
            //and any other checks for user
        },
        // OnAuthenticationFailed = async context =>{
        //     if(!context.HttpContext.Response.HasStarted){
        //       await context.HttpContext.Response.WriteAsJsonAsync(new {message="UnAuthorized Access"});
        //     }
           
        // }        
    };
    options.IncludeErrorDetails = true;
});

builder.Services.AddHealthChecks()
    // .AddNpgSql(Configuration.GetConnectionString("DatabaseConnection")!)
    .AddSqlite(Configuration.GetConnectionString("DatabaseConnection")!,healthQuery:"select * from users where id='1'" );


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

app.MapHealthChecks(
    "/health", new HealthCheckOptions {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    }
);

app.UseCors();

app.UseExceptionHandler("/error");

app.UseAuthentication();

app.UseAuthorization();

app.UseCookiePolicy();

app.UseSession();

app.UseResponseCaching();

app.MapControllers();

app.Run();
