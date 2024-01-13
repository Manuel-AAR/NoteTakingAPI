using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Linq;

namespace backend.Models;

public class NoteTag
{
    [Key]
    public int NoteTagId { get; set; }

    [Required]
    public int NoteId { get; set; }

    [Required]
    public int TagId { get; set; }
    
    [ForeignKey("NoteId")]
    [JsonIgnore]
    public Note? Note { get; set; }

    [ForeignKey("TagId")]
    [JsonIgnore]
    public Tag? Tag { get; set; }
}