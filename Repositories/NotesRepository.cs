using backend.Models;
using backend.Data;
using backend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class NoteRepository
{
    public ExerciseDBContext context;

    public NoteRepository ()
    {
        context = new();
    }

    public List<NoteDTO> GetNotes (int UserId)
    {
        List<Note> notes = context.Notes.Where(Note => Note.UserId == UserId && !Note.Archived).ToList() ?? [];
        if (notes.Count <= 0) throw new Exception("This user doesn't have Notes");
        List<NoteDTO> noteDTOs = notes.Select(x => new NoteDTO(x)).ToList();
        return noteDTOs;
    }

    public List<NoteDTO> GetArchivedNotes (int UserId)
    {
        List<Note> notes = context.Notes.Where(Note => Note.UserId == UserId && Note.Archived).ToList() ?? [];
        if (notes.Count <= 0) throw new Exception("This user doesn't have Archived Notes");
        List<NoteDTO> noteDTOs = notes.Select(x => new NoteDTO(x)).ToList();
        return noteDTOs;
    }

    public Note GetNote (int UserId, int NoteId)
    {
        Note note = context.Notes.Include(n => n.Tags).FirstOrDefault(Note => Note.UserId == UserId && Note.NoteId == NoteId) ?? new();
        if (note.NoteId != NoteId || note.NoteId <= 0) throw new Exception("The specified Note does not exist");
        return note;
    }

    public List<NoteDTO> SearchNotes (int UserId, List<int> tagIds)
    {
        if (tagIds.Count <= 0) throw new Exception("Invalid Input");
        List<Note> notes = context.Notes.Include(n => n.Tags).Where(n => n.Tags != null && n.Tags.Count != 0 && !n.Archived && n.UserId == UserId).ToList() ?? [];
        
        if (notes.Count <= 0) throw new Exception("This user notes don't match the filters");
        notes = notes.Where(n => n.Tags != null && tagIds.All(tagId => n.Tags.Any(nt => nt.TagId == tagId))).ToList() ?? [];

        if (notes.Count <= 0) throw new Exception("This user notes don't match the filters");
        List<NoteDTO> noteDTOs = notes.Select(x => new NoteDTO(x)).ToList();
        return noteDTOs;
    }

    public List<NoteDTO> SearchNotesArchived (int UserId, List<int> tagIds)
    {
        if (tagIds.Count <= 0) throw new Exception("Invalid Input");
        List<Note> notes = context.Notes.Include(n => n.Tags).Where(n => n.Tags != null && n.Tags.Count != 0 && n.Archived && n.UserId == UserId).ToList() ?? [];
        
        if (notes.Count <= 0) throw new Exception("This user notes don't match the filters");
        notes = notes.Where(n => n.Tags != null && tagIds.All(tagId => n.Tags.Any(nt => nt.TagId == tagId))).ToList() ?? [];

        if (notes.Count <= 0) throw new Exception("This user notes don't match the filters");
        List<NoteDTO> noteDTOs = notes.Select(x => new NoteDTO(x)).ToList();
        return noteDTOs;
    }

    public void CreateNote (Note newNote)
    {
        context.Notes.Add(newNote);

        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem creating the Note");
    }

    public void UpdateNote (Note updateNote)
    {
        Note note = context.Notes.FirstOrDefault(Note => Note.UserId == updateNote.UserId && Note.NoteId == updateNote.NoteId) ?? new();
        if (note.NoteId != updateNote.NoteId || note.NoteId <= 0) throw new Exception("The specified Note does not exist");

        note.NoteName = updateNote.NoteName;
        note.NoteContent = updateNote.NoteContent;
        note.NoteDate = DateTime.Now;

        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem updating the Note");
    }

    public void ArchiveNote (int UserId, int NoteId)
    {
        Note note = context.Notes.FirstOrDefault(Note => Note.UserId == UserId && Note.NoteId == NoteId) ?? new();
        if (note.NoteId != NoteId) throw new Exception("The specified Note does not exist");
        note.Archived = !note.Archived;
        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem archiving the Note");
    }

    public void DeleteNote (int UserId, int NoteId)
    {
        Note note = context.Notes.FirstOrDefault(Note => Note.UserId == UserId && Note.NoteId == NoteId) ?? new();
        if (note.NoteId != NoteId) throw new Exception("The specified Note does not exist");
        context.Remove(note);
        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem archiving the Note");
    }




}