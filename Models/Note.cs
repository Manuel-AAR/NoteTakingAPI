using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json.Linq;

namespace backend.Models;

public class Note
{
    [Key]
    public int NoteId {get; set;}

    [Required]
    public int UserId {get; set;}
    
    [StringLength(30)]
    public string NoteName {get; set;} = string.Empty;
    
    [Required]
    public string NoteContent {get; set;} = string.Empty;

    [Required]
    public DateTime NoteDate {get; set;} = DateTime.Now;

    [Required]
    public bool Archived {get; set;} = false;

    [ForeignKey("UserId")]
    public User? User {get; set;}

    public ICollection<NoteTag>? Tags {get; set;}
}