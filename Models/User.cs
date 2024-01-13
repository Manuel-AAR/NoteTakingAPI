using System.ComponentModel.DataAnnotations;

namespace backend.Models;

public class User
{
    [Key]
    public int UserId {get; set;}

    [Required]
    [StringLength(30)]
    public string UserName {get; set;} = string.Empty;

    [Required]
    [StringLength(64)]
    public string Password {get; set;} = string.Empty;
}