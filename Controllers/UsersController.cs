using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using backend.Models.DTOs;
using backend.Models;
using backend.Data;
using backend.Services;
using backend.Repositories;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : Controller 
{

    public UserRepository userRepository;

    public UserController ()
    {
        userRepository = new();
    }

    [HttpPost("LogIn")]
    public IActionResult LogIn ([FromBody] LogIn logIn)
    {
        User user = new() 
        {
            UserName = logIn.UserName,
            Password = HashingService.Hash256(logIn.Password)
        };
        
        return Ok(userRepository.LogIn(user));
        
    }

    [HttpPost("CreateUser")]
    public IActionResult CreateUser ([FromBody] CreateUser createUser)
    {
        if (createUser.Password != createUser.PasswordCheck) throw new Exception("The passwords don't match");

        User user = new() 
        {
            UserName = createUser.UserName,
            Password = HashingService.Hash256(createUser.Password)
        };
        
        userRepository.CreateUser(user);
        return Ok();
    }
}