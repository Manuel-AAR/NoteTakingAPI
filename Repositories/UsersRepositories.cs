using backend.Data;
using backend.Models.DTOs;
using backend.Models;
namespace backend.Repositories;

public class UserRepository
{
    public ExerciseDBContext context;

    public UserRepository ()
    {
        context = new();
    }

    public int LogIn (User logIn)
    {
        User user = context.Users.FirstOrDefault(User => User.UserName == logIn.UserName && User.Password == logIn.Password) ?? new();        
        if (user.UserId <= 0) throw new Exception("Incorrect UserName or Password");
        return user.UserId;
    }
    public void CreateUser (User newUser)
    {
       
        User user = context.Users.FirstOrDefault(User => User.UserName == newUser.UserName) ?? new();

        if (user.UserId > 0) throw new Exception("User Already Exist");

        context.Users.Add(newUser);

        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem creating the user");
    }
}