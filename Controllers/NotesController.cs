using Microsoft.AspNetCore.Mvc;
using backend.Services;
using backend.Models.DTOs;
using backend.Repositories;
using backend.Models;


namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class NoteController : Controller
{
    public NoteRepository noteRepository;
    public NoteController ()
    {
        noteRepository = new();
    }

    [HttpGet("GetNotes")]
    public IActionResult GetNotes ()
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.GetNotes(UserId));
    }

    [HttpGet("GetArchivedNotes")]
    public IActionResult GetArchivedNotes ()
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.GetArchivedNotes(UserId));
    }

    [HttpGet("GetNote")]
    public IActionResult GetNote ([FromQuery] int NoteId)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.GetNote(UserId, NoteId));
    }

    [HttpPost("SearchNotes")]
    public IActionResult SearchNotes([FromBody] Search search)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.SearchNotes(UserId, search.TagIDs));

    }

    [HttpPost("SearchNotesArchived")]
    public IActionResult SearchNotesArchived([FromBody] Search search)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.SearchNotesArchived(UserId, search.TagIDs));

    }

    [HttpPost("CreateNote")]
    public IActionResult CreateNote([FromBody] CreateNote createNote)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        Note note = new()
        {
            NoteName = createNote.NoteName,
            NoteContent = createNote.NoteContent,
            UserId = UserId,
            NoteDate = DateTime.Now
        };

        noteRepository.CreateNote(note);
        return Ok();
    }

    [HttpPatch("UpdateNote")]
    public IActionResult UpdateNote ([FromBody] UpdateNote updateNote)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        Note note = new()
        {
            UserId = UserId,
            NoteId = updateNote.NoteId,
            NoteName = updateNote.NoteName,
            NoteContent = updateNote.NoteContent
        };

        noteRepository.UpdateNote(note);
        return Ok();
    }

    [HttpPatch("ArchiveNote")]
    public IActionResult ArchiveNote ([FromBody] GetById NoteId)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        noteRepository.ArchiveNote(UserId, NoteId.Id);
        return Ok();
    }

    [HttpDelete("DeleteNote")]
    public IActionResult DeleteNote ([FromBody] GetById NoteId)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        noteRepository.DeleteNote(UserId, NoteId.Id);
        return Ok();
    }
}