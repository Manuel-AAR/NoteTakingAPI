namespace backend.Models.DTOs;

public class CreateNote
{
    public string NoteName {get; set;} = string.Empty;

    public string NoteContent {get; set;} = string.Empty;
}