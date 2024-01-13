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

    /// <summary>
    /// Performs a logIn.
    /// </summary>
    /// <param name="logIn">A DTO for the login credentials</param>
    /// <returns>
    /// Returns an IActionResult containing the user id.
    /// </returns>
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

    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="createUser">A DTO for the new user credentials</param>
    /// <returns>
    /// Returns an IActionResult.
    /// </returns>
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