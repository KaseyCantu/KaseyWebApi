using System.ComponentModel.DataAnnotations.Schema;

namespace KaseyWebApi.DataModel;

[Table("links")]
public class Link
{
    public Link()
    {
    }

    public Link(string? id, string? linkUrl, string? description, string? topic, string createdAt)
    {
        this.Id = id;
        this.LinkUrl = linkUrl;
        this.Description = description;
        this.Topic = topic;
        this.CreatedAt = createdAt;
    }

    public string? Id { get; set; }

    public string? LinkUrl { get; set; }

    public string? Description { get; set; }

    public string? Topic { get; set; }

    public string CreatedAt { get; set; }
}

public class NewLink
{
    public string? LinkUrl { get; set; }

    public string? Description { get; set; }

    public string? Topic { get; set; }
}