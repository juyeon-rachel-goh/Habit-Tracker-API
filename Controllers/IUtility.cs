using Api.Models;
using Microsoft.AspNetCore.Identity;


namespace Api.Controllers;


public interface IUtility
{
    public Task<IdentityUser> GetContextUser(HttpContext context);

}