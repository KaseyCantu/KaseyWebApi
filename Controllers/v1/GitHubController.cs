using Microsoft.AspNetCore.Mvc;
using KaseyWebApi.ClientServices;
using KaseyWebApi.DataModel.GitHubResponseTypes;

namespace KaseyWebApi.Controllers.v1;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class GitHubController : Controller
{
    private readonly GitHubService _gitHubService;

    public GitHubController(GitHubService gitHubService)
    {
        _gitHubService = gitHubService;
    }

    // GET GitHub users test usage of the Depenacy Injection built into C#
    [HttpGet("user")]
    public async Task<IActionResult> GetGitHubUsers()
    {
        try
        {
            GitHubUser? responseBody = await _gitHubService.GetCurrentUser();
            return Ok(responseBody);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("\nFailure to execute github user HTTP request!");
            Console.WriteLine("Message: {0} ", ex.Message);
            return Problem(
                ex.Message,
                nameof(GitHubController),
                (int?)ex.StatusCode,
                "Fetching GitHub user failed",
                nameof(HttpRequestException)
            );
        }
    }
}