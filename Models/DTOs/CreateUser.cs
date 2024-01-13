namespace backend.Models.DTOs;

public class CreateUser
{
    public string UserName {get; set;} = string.Empty;
    public string Password {get; set;} = string.Empty;
    public string PasswordCheck {get; set;} = string.Empty;
}