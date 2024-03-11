using DataAPI.Data;
using DataAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Services;

public interface IUserService
{
    IActionResult GetAllUsers();
    User? FetchUser(string email);
    IActionResult GetUser(int id);
    IActionResult RegisterUser(RegisterUser registerUser);
    IActionResult LoginUser(LoginUser user);
    IActionResult DeleteUser(int id);
}