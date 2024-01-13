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

    /// <summary>
    /// This method is used to get notes.
    /// </summary>
    /// <returns>
    /// Returns an IActionResult that contains the notes for a specific user.
    /// </returns>
    /// <exception cref="System.Exception">Thrown when the UserId in the request header is invalid.</exception>
    [HttpGet("GetNotes")]
    public IActionResult GetNotes ()
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.GetNotes(UserId));
    }

    /// <summary>
    /// This method is used to get the archived notes for a specific user.
    /// </summary>
    /// <returns>
    /// Returns an IActionResult that contains the list of archived notes for the user.
    /// </returns>
    /// <exception cref="Exception">Thrown when the UserId in the request headers is invalid.</exception>
    [HttpGet("GetArchivedNotes")]
    public IActionResult GetArchivedNotes ()
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.GetArchivedNotes(UserId));
    }

    /// <summary>
    /// This method is used to get a specific note by it's ID
    /// </summary>
    /// <param name="NoteId">The id of the note to get</param>
    /// <returns>
    /// Returns an IActionResult that contains the data that belongs to the note requested
    /// </returns>
    /// <exception cref="Exception">Thrown when the UserId in the request headers is invalid.</exception>
    [HttpGet("GetNote")]
    public IActionResult GetNote ([FromQuery] int NoteId)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.GetNote(UserId, NoteId));
    }
    
    /// <summary>
    /// Does a search that gets the notes that match all the tags provided
    /// </summary>
    /// <param name="search">A list of the id's of the selected tags</param>
    /// <returns>
    /// Returns an IActionResult that contains the list of notes that match the tags provided.
    /// </returns>
    [HttpPost("SearchNotes")]
    public IActionResult SearchNotes([FromBody] Search search)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.SearchNotes(UserId, search.TagIDs));

    }

    /// <summary>
    /// Does a search that gets the archived notes that match all the tags provided.
    /// </summary>
    /// <param name="search">A list of the id's of the selected tags</param>
    /// <returns>
    /// Returns an IActionResult that contains the list of archived notes that match the tags provided.
    /// </returns>
    [HttpPost("SearchNotesArchived")]
    public IActionResult SearchNotesArchived([FromBody] Search search)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        return Ok(noteRepository.SearchNotesArchived(UserId, search.TagIDs));

    }

    /// <summary>
    /// Creates a new note
    /// </summary>
    /// <param name="createNote">A DTO for the data of the new note </param>
    /// <returns>
    /// An IActionResult
    /// </returns>
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

    /// <summary>
    /// Updates an existing note
    /// </summary>
    /// <param name="updateNote">A DTO for the updated note data</param>
    /// <returns>
    /// An IActionResult
    /// </returns>
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

    /// <summary>
    /// Archives a note
    /// </summary>
    /// <param name="NoteId">The Id of the note to archive</param>
    /// <returns>
    /// An IActionResult
    /// </returns>
    [HttpPatch("ArchiveNote")]
    public IActionResult ArchiveNote ([FromBody] GetById NoteId)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        noteRepository.ArchiveNote(UserId, NoteId.Id);
        return Ok();
    }

    /// <summary>
    /// Deletes a note.
    /// </summary>
    /// <param name="NoteId">The Id of the note to delete</param>
    /// <returns>
    /// An IActionResult
    /// </returns>
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