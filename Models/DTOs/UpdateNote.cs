namespace backend.Models.DTOs;

public class UpdateNote
{
    public int NoteId {get; set;}

    public string NoteName {get; set;} = string.Empty;

    public string NoteContent {get; set;} = string.Empty;
}