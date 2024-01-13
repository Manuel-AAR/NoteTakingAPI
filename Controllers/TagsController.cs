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

    [HttpGet("GetTags")]
    public IActionResult GetTags ()
    {
        HeaderExtractor headerExtractor = new(HttpContext);
        var stringUserId = headerExtractor.ExtractHeaders("UserId");
        if (!int.TryParse(stringUserId, out int UserId)) throw new Exception("Invalid data in Request");

        
        return Ok(tagRepository.GetTags(UserId));
        
    }

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

    [HttpPatch("AddTagToNote")]
    public IActionResult AddTagToNote ([FromBody] AddTagToNote addTagToNote)
    {

        NoteTag noteTag = new()
        {
            NoteId = addTagToNote.NoteId,
            TagId = addTagToNote.TagId
        };

        tagRepository.AddTagToNote(noteTag);
        return Ok();
    }

    [HttpPatch("RemoveTagFromNote")]
    public IActionResult RemoveTagFromNote ([FromBody] AddTagToNote addTagToNote)
    {

        NoteTag noteTag = new()
        {
            NoteId = addTagToNote.NoteId,
            TagId = addTagToNote.TagId
        };

        tagRepository.RemoveTagFromNote(noteTag);
        return Ok();
    }

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