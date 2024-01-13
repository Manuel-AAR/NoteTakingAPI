using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace backend.Models;

public class Tag
{
    [Key]
    public int TagId {get; set;}

    [Required]
    public int UserId {get; set;}

    [Required]
    [StringLength(30)]
    public string TagName {get; set;} = string.Empty;

    [ForeignKey("UserId")]
    public User? User {get; set;}

    public ICollection<NoteTag>? Notes {get; set;}
}