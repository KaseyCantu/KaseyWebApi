using KaseyWebApi.Context;
using KaseyWebApi.DataModel;
using Microsoft.AspNetCore.Mvc;

namespace KaseyWebApi.Controllers.v1;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class LinksController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public LinksController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbContext.Database.EnsureCreated();
    }

    // GET api/Links
    [HttpGet]
    public IActionResult GetLinks()
    {
        return Ok(_dbContext.Links?.ToList());
    }

    // POST api/Links
    [HttpPost]
    public IActionResult PostLink([FromBody] NewLink newLink)
    {
        try
        {
            _dbContext.Links?.Add(new Link
                {
                    Id = Guid.NewGuid().ToString(),
                    LinkUrl = newLink.LinkUrl,
                    Description = newLink.Description,
                    Topic = newLink.Topic,
                    CreatedAt = DateTime.Now.ToString()
                }
            );

            _dbContext.SaveChanges();

            return Ok(new { success = true });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e.Message);
        }
    }

    // GET api/Links/<linkId>
    // https://docs.microsoft.com/en-us/ef/core/querying/
    [HttpGet("{linkId}")]
    public IActionResult GetLinkById([FromRoute] string linkId)
    {
        try
        {
            var linkById = _dbContext.Links?.Where(link => link.Id == linkId).First();
            return Ok(linkById);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}