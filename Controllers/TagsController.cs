using Microsoft.AspNetCore.Mvc;
using backend.Data;
using backend.Services;
using backend.Models.DTOs;
using backend.Repositories;
using backend.Models;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class TagController : Controller
{

    public TagRepository tagRepository;
    public TagController ()
    {
        
        tagRepository = new();
    }

    /// <summary>
    /// This method is used to get tags.
    /// </summary>
    /// <returns>
    /// Returns an IActionResult that contains the tags for a specific user.
    /// </returns>
    [HttpGet("GetTags")]
    public IActionResult GetTags ()
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");

        
        return Ok(tagRepository.GetTags(UserId));
        
    }

    /// <summary>
    /// Creates a tag.
    /// </summary>
    /// <param name="tagName">A DTO for the data of the new tag</param>
    /// <returns>
    /// Returns an IActionResult.
    /// </returns>
    [HttpPost("CreateTag")]
    public IActionResult CreateTag ([FromBody] CreateTag tagName)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");
        
        Tag tag = new()
        {
            TagName = tagName.TagName,
            UserId = UserId
        };
        tagRepository.CreateTag(tag);
        return Ok();
    }

    /// <summary>
    /// Adds a tag to a note
    /// </summary>
    /// <param name="TagAndNote">A DTO containing the id of the note and the tag respectively</param>
    /// <returns>
    /// Returns an IActionResult.
    /// </returns>
    [HttpPatch("AddTagToNote")]
    public IActionResult AddTagToNote ([FromBody] TagAndNote TagAndNote)
    {

        NoteTag noteTag = new()
        {
            NoteId = TagAndNote.NoteId,
            TagId = TagAndNote.TagId
        };

        tagRepository.TagAndNote(noteTag);
        return Ok();
    }

    /// <summary>
    /// Removes a tag from a note
    /// </summary>
    /// <param name="TagAndNote">A DTO containing the id of the note and the tag respectively</param>
    /// <returns>
    /// Returns an IActionResult.
    /// </returns>
    [HttpPatch("RemoveTagFromNote")]
    public IActionResult RemoveTagFromNote ([FromBody] TagAndNote TagAndNote)
    {

        NoteTag noteTag = new()
        {
            NoteId = TagAndNote.NoteId,
            TagId = TagAndNote.TagId
        };

        tagRepository.RemoveTagFromNote(noteTag);
        return Ok();
    }

    /// <summary>
    /// Deletes a Tag.
    /// </summary>
    /// <param name="TagId">The id of the tag to delete</param>
    /// <returns>
    /// Returns an IActionResult.
    /// </returns>
    [HttpDelete("DeleteTag")]
    public IActionResult DeleteTag ([FromBody] GetById TagId)
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");

        tagRepository.DeleteTag(UserId, TagId.Id);
        return Ok();
    }

}