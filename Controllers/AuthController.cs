using Api.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
// using Newtonsoft.Json;

namespace Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private ILogger<AuthController> _logger;
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;

    public AuthController(ILogger<AuthController> logger, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _logger = logger;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpGet]
    [Route("")]
    public OkObjectResult Get()
    {
        return Ok("hellooo");
    }


    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody] UserRegister userRegister)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = userRegister.UserName, Email = userRegister.Email };
            var result = await _userManager.CreateAsync(user, userRegister.Password);
            if (result.Succeeded)
            {
                _logger.LogInformation("User created a new account with password.");

                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok("ok");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return BadRequest(ModelState.Values);
    }


    [HttpPost]
    [Route("signin")]
    public async Task<IActionResult> SignIn([FromBody] UserRegister userSignin)
    {
        if (ModelState.IsValid)
        {
            var result = await _signInManager.PasswordSignInAsync(userSignin.UserName, userSignin.Password, true, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
        return BadRequest();
    }
}
