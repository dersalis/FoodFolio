using FoodFolio.WebApi.Dtos;
using FoodFolio.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FoodFolio.WebApi.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    public readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDto registerUser)
    {
        await _accountService.RegisterUserAsync(registerUser);
        return Ok();
    }


    [HttpPost("login")]
    public ActionResult Login([FromBody] LoginDto login)
    {
        string token = _accountService.GenerateJwt(login);

        return Ok(token);
    }
}

