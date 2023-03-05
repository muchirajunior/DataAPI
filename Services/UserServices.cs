using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using DataAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

    public IActionResult GetAllUsers()=> new OkObjectResult(databaseContext.Users!.ToList());
    public IActionResult GetUser(int id)=> new OkObjectResult(databaseContext.Users!.Where(user=>user.ID==id).Include(user=>user.UserBusiness).Include(user=>user.UserBusiness!.Products).FirstOrDefault());
    public IActionResult RegisterUser(RegisterUser registerUser){
        try {
            User user=new User(){FullName=registerUser.Name, Password=registerUser.Password,Username=registerUser.Username,Role=registerUser.Role,Email=registerUser.Email};
            user.Password=new PasswordHasher<Object?>().HashPassword(null,user.Password);
            databaseContext.Add(user);
            databaseContext.SaveChanges();
            // SendEmail(user.Email!);
            return new CreatedResult("",user);
        }catch (Exception error){
            return new BadRequestObjectResult(new {message="failed to update", error=error.InnerException!.ToString()});
        }

    }

    public IActionResult LoginUser(LoginUser loginUser){
        User? user = databaseContext.Users!.Where(usr=> usr.Username==loginUser.Username).FirstOrDefault();
        if (user==null){
            return new NotFoundObjectResult(new {login=false,message="user does not exist in the system"});
        }
        PasswordVerificationResult result=new PasswordHasher<Object?>().VerifyHashedPassword(null,user.Password,loginUser.Password);
        if (result==PasswordVerificationResult.Success){
            return new OkObjectResult(new {login=true,user,token=GenerateJSONWebToken(user)});
        }

        if(result==PasswordVerificationResult.Failed){
            return new BadRequestObjectResult(new {login=false,message="incorrect password !"});
        }

        return new BadRequestObjectResult(new  {login=false,message="system error !"});
    } 

    public IActionResult DeleteUser(int id){
        User? usr=databaseContext.Users!.Where(user=>user.ID==id).FirstOrDefault();
        if (usr==null){
            return new BadRequestObjectResult(new {message="failed to delete, user does not exist"});
        }
        databaseContext.Remove(usr);
        databaseContext.SaveChanges();
        return new OkObjectResult(new {message="deleted user successfully"});
    }

    private void SendEmail(string email){  
        try{
           var smtpClient = new SmtpClient("smtp.gmail.com"){
                Port = 465,//587,
                Credentials = new NetworkCredential("nduati.muchira@s.karu.ac.ke", "*****"),
                EnableSsl = true,
            };
            smtpClient.Send("test@email.com", email, "email subject", "This is a test a email for smtp");
            Console.WriteLine("Email sent"); 
        }catch (System.Exception error){
         Console.WriteLine(error);
        }
    }  

    private string GenerateJSONWebToken(User user){
        SymmetricSecurityKey securityKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:SignKey"]!)); 
        SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);  

        List<Claim> AuthClaims=new List<Claim>{
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.UniqueName,user.Username!),
                new Claim(JwtRegisteredClaimNames.GivenName,user.FullName!),
                new Claim(JwtRegisteredClaimNames.Email,user.Email!),
                new Claim(ClaimTypes.Role,user.Role!),
                new Claim(JwtRegisteredClaimNames.Sub,""),//for python APIs
            };

        JwtSecurityToken? token = new JwtSecurityToken(
                        issuer: configuration["JwtSettings:validIssuer"],   
                        audience: configuration["JwtSettings:validAudience"],   
                        claims: AuthClaims,
                        expires: DateTime.Now.AddMinutes(120),    
                        signingCredentials: credentials
                    );    

        return new JwtSecurityTokenHandler().WriteToken(token); 
    }

}