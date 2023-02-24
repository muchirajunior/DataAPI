using DataAPI.Models;

namespace DataAPI.Services;

public interface IUserService
{
    dynamic GetAllUsers();
    dynamic GetUser(int id);
    dynamic RegisterUser(User user);
    dynamic LoginUser(LoginUser user);
    dynamic DeleteUser(int id);
}