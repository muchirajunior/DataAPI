using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

    public dynamic GetAllUsers()=> Results.Ok(databaseContext.Users!.ToList());
    public dynamic GetUser(int id)=> Results.Ok(databaseContext.Users!.Where(user=>user.ID==id).Include(user=>user.UserBusiness).Include(user=>user.UserBusiness!.Products).FirstOrDefault());
    public dynamic RegisterUser(User user){
        try {
            user.Password=new PasswordHasher<Object?>().HashPassword(null,user.Password);
            databaseContext.Add(user);
            databaseContext.SaveChanges();
            SendEmail(user.Email!);
            return Results.Created("",user);
        }catch (System.Exception error){
            return Results.BadRequest(new {error,message="process failed!"});
        }

    }

    public dynamic LoginUser(LoginUser loginUser){
        User? user = databaseContext.Users!.Where(usr=> usr.Username==loginUser.Username).FirstOrDefault();
        if (user==null){
            return new {login=false,error="user does not exist in the system"};
        }
        PasswordVerificationResult result=new PasswordHasher<Object?>().VerifyHashedPassword(null,user.Password,loginUser.Password);
        if (result==PasswordVerificationResult.Success){
            return Results.Ok(new {login=true,user,token=GenerateJSONWebToken(user)});
        }

        if(result==PasswordVerificationResult.Failed){
            return Results.BadRequest(new {login=false,error="incorrect password !"});
        }

        return Results.BadRequest(new  {login=false,error="system error !"});
    } 

    public dynamic DeleteUser(int id){
        User? usr=databaseContext.Users!.Where(user=>user.ID==id).FirstOrDefault();
        if (usr==null){
            return Results.BadRequest(new {message="failed to delete, user does not exist"});
        }
        databaseContext.Remove(usr);
        databaseContext.SaveChanges();
        return Results.Ok(new {message="deleted user successfully"});
    }

    private void SendEmail(string email){  
        try{
           var smtpClient = new SmtpClient("smtp.gmail.com"){
                Port = 465,//587,
                Credentials = new NetworkCredential("nduati.muchira@s.karu.ac.ke", "37081214"),
                EnableSsl = true,
            };
            smtpClient.Send("test@email.com", email, "email subject", "This is a test a email for smtp");
            Console.WriteLine("Email sent"); 
        }catch (System.Exception error){
         Console.WriteLine(error);
        }
    }  

    private string GenerateJSONWebToken(User user){
        SymmetricSecurityKey? securityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SignKey"])); 
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  

        List<Claim> AuthClaims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Username!),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FullName!),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim("sub","") //for python apis
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