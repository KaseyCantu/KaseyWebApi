using KaseyWebApi.DataModel.GitHubResponseTypes;
using Microsoft.Net.Http.Headers;

namespace KaseyWebApi.ClientServices;

public class GitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService(IConfiguration configuration, HttpClient httpClient)
    {
        var _configuration = configuration;

        _httpClient = httpClient;

        _httpClient.BaseAddress = new Uri(_configuration["BaseUrls:GitHubApi"]);

        // The GitHub API requires two headers.
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Authorization, _configuration["ApiKeys:GitHubAccessToken"]
        );

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/vnd.github.v3+json");

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.UserAgent, DeriveUserAgent());
    }

    public async Task<GitHubUser?> GetCurrentUser()
    {
        return await _httpClient.GetFromJsonAsync<GitHubUser>("/user");
    }

    public string DeriveUserAgent()
    {
        return $"{Environment.MachineName} - {Environment.OSVersion}";
    }
}