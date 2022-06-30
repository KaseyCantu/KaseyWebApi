using KaseyWebApi.DataModel;
using KaseyWebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KaseyWebApi.Controllers.v1;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces("application/json")]
public class UsersController : Controller
{
    private readonly IUsers _usersContext;

    public UsersController(IUsers usersContext)
    {
        _usersContext = usersContext;
    }

    // GET api/v1/Users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserInfo>>> Get()
    {
        return await Task.FromResult(_usersContext.GetUsers());
    }

    // GET api/v1/Users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserInfo>> GetUserById(int id)
    {
        var users = await Task.FromResult(_usersContext.GetUserById(id));
        if (users == null)
        {
            return NotFound();
        }

        return users;
    }

    // POST api/v1/Users
    [HttpPost]
    public async Task<ActionResult<UserInfo>> Post(UserInfo user)
    {
        _usersContext.CreateUser(user);
        return await Task.FromResult(user);
    }

    // PUT api/v1/Users/5
    [HttpPut("{id}")]
    public async Task<ActionResult<UserInfo>> Put(int id, UserInfo user)
    {
        if (id != user.UserId)
        {
            return NotFound("No user found with that Id");
        }

        try
        {
            _usersContext.UpsertUser(user);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }

            Console.WriteLine(ex);
            return BadRequest(ex.Message);
        }

        return await Task.FromResult(user);
    }

    // DELETE api/v1/Users/7
    [HttpDelete("{id}")]
    public async Task<ActionResult<UserInfo>> Delete(int id)
    {
        var user = _usersContext.DeleteEmployee(id);
        return await Task.FromResult(user);
    }

    private bool UserExists(int id)
    {
        return _usersContext.CheckUser(id);
    }
}