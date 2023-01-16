
using Api.Data;
using Microsoft.AspNetCore.Identity;

namespace Api.Controllers;

public class Utility : IUtility

{
    private ApiDbContext context;

    private UserManager<IdentityUser> _userManager;
    public Utility(ApiDbContext context, UserManager<IdentityUser> userManager)
    {
        this.context = context;
        _userManager = userManager;
    }

    async public Task<IdentityUser> GetContextUser(HttpContext context)
    {
        var currentUser = (await this._userManager.GetUserAsync(context.User));
        return currentUser;
    }


}