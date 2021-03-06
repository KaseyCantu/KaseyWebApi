using KaseyWebApi.Context;
using KaseyWebApi.DataModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KaseyWebApi.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
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
    public ActionResult<Link> GetLinks()
    {
        return Ok(_dbContext.Links?.ToList());
    }

    // POST api/Links
    [HttpPost]
    public ActionResult<Link> PostLink([FromBody] NewLink newLink)
    {
        try
        {
            _dbContext.Links?.Add(new Link
                {
                    LinkId = Guid.NewGuid().ToString(),
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
    public ActionResult<Link> GetLinkById([FromRoute] string linkId)
    {
        try
        {
            var linkById = _dbContext.Links?.First(link => link.LinkId == linkId);
            return Ok(linkById);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return Conflict(e);
        }
    }
}