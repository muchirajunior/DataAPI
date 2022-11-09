using DataAPI.Models;

namespace DataAPI.Services;

public interface IUserService{
    dynamic RegisterUser(User user);
    dynamic LoginUser(LoginUser user);
}