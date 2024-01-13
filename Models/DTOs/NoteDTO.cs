namespace backend.Models.DTOs;

public class NoteDTO(Note note)
{
    public int NoteId {get; set;} = note.NoteId;
    public string NoteName {get; set;} = note.NoteName;
    public DateTime NoteDate {get; set;} = note.NoteDate;
    public bool Archived {get; set;} = note.Archived;

}