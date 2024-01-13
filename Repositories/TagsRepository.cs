using backend.Models;
using backend.Data;
using backend.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories;

public class TagRepository
{
    public ExerciseDBContext? context;

    public List<TagDTO> GetTags (int UserId)
    {
        context = new();
        List<Tag> tags = context.Tags.Where(Tag => Tag.UserId == UserId).ToList();
        if (tags.Count <= 0) throw new Exception("This user doesn't have Tags");
        List<TagDTO> tagDTOs = tags.Select(x => new TagDTO(x)).ToList();
        return tagDTOs;
    }

    public void CreateTag (Tag newTag)
    {
        context = new();
        Tag tag = context.Tags.FirstOrDefault(Tag => Tag.TagName == newTag.TagName) ?? new();

        if (tag.TagId > 0) throw new Exception("Tag already exist");

        context.Tags.Add(newTag);

        int affectedRows = context.SaveChanges();

        context.Entry(newTag).State = EntityState.Detached;

        if (affectedRows <= 0) throw new Exception("There was a problem creating the tag");

    }

    public void TagAndNote (NoteTag TagAndNote)
    {
        context = new();
        NoteTag noteTag = context.NoteTags.FirstOrDefault(NoteTag => NoteTag.NoteId == TagAndNote.NoteId && NoteTag.TagId == TagAndNote.TagId) ?? new();
        if (noteTag.NoteTagId > 0) throw new Exception("The tag it's already linked to that note");

        context.NoteTags.Add(TagAndNote);

        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem linking the tag to the note");
    }

    public void RemoveTagFromNote (NoteTag removeTagFromNote)
    {
        context = new();
        NoteTag noteTag = context.NoteTags.FirstOrDefault(NoteTag => NoteTag.NoteId == removeTagFromNote.NoteId && NoteTag.TagId == removeTagFromNote.TagId) ?? new();
        if (noteTag.NoteTagId <= 0) throw new Exception("The tag it's not linked to that note");

        context.Remove(noteTag);

        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem linking the tag to the note");
    }

    public void DeleteTag (int UserId, int TagId)
    {
        context = new();
        Tag tag = context.Tags.Include(t => t.Notes).FirstOrDefault(Tag => Tag.TagId == TagId && Tag.UserId == UserId) ?? new();
        if (tag.TagId != TagId || tag.TagId <= 0) throw new Exception("The specified Tag does not exist: u: " + UserId.ToString() + "// tag: " + TagId.ToString());
        
        var state = context.Entry(tag).State;
        if (state == EntityState.Detached || state == EntityState.Added)
        {
            throw new Exception("The tag has a temporary key value.");
        }
        context.Remove(tag);

        int affectedRows = context.SaveChanges();

        if (affectedRows <= 0) throw new Exception("There was a problem deleting the tag");
    }
}