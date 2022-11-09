using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace DataAPI.Services;

public class UserServices  : IUserService{
    private readonly DatabaseContext databaseContext;
    private readonly IConfiguration configuration;

    public UserServices(DatabaseContext databaseContext, IConfiguration configuration)
    {
        this.databaseContext = databaseContext;
        this.configuration = configuration;
    }
    public dynamic RegisterUser(User user){
        try {
            user.Password=new PasswordHasher<Object?>().HashPassword(null,user.Password);
            databaseContext.Add(user);
            databaseContext.SaveChanges();
            return new {complete=true,user};
        }catch (System.Exception error){
            return new {complete=false,error};
        }

    }

    public dynamic LoginUser(LoginUser loginUser){
        User? user = databaseContext.Users!.Where(usr=> usr.Username==loginUser.Username).FirstOrDefault();
        if (user==null){
            return new {login=false,error="user does not exist in the system"};
        }
        PasswordVerificationResult result=new PasswordHasher<Object?>().VerifyHashedPassword(null,user.Password,loginUser.Password);
        if (result==PasswordVerificationResult.Success){
            return new {login=true, user,token=GenerateJSONWebToken(user)};
        }

        if(result==PasswordVerificationResult.Failed){
            return new {login=false,error="incorrect password !"};
        }

        return new {login=false,error="system error !"};
    }

    private string GenerateJSONWebToken(User user){
        SymmetricSecurityKey? securityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SignKey"])); 
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  

        List<Claim> AuthClaims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Username!),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FullName!),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!)
            };

        JwtSecurityToken? token = new JwtSecurityToken(
                        issuer: configuration["Jwt:validIssuer"],   
                        audience: configuration["Jwt:validAudience"],   
                        claims: AuthClaims,
                        expires: DateTime.Now.AddMinutes(120),    
                        signingCredentials: credentials
                    );    

        return new JwtSecurityTokenHandler().WriteToken(token); 
    }

}