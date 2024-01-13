namespace backend.Models.DTOs;

public class TagDTO(Tag tag)
{
    public int TagId {get; set;} = tag.TagId;
    public string TagName {get; set;} = tag.TagName;

}