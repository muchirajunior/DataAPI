using DataAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace DataAPI.Services;

public interface IAuthService{
    Task<IActionResult> RegisterUser(RegisterUser user);
}