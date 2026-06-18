using Application.Services;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services;

public class UserManager(UserManager<ApplicationUser> userManager) : IUserManager
{

}