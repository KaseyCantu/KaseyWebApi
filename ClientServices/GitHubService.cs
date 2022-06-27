using Microsoft.Net.Http.Headers;
using KaseyWebApi.DataModel.GitHubResponseTypes;

namespace KaseyWebApi.ClientServices;

public class GitHubService
{
    private readonly HttpClient _httpClient;

    public GitHubService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        
        _httpClient.BaseAddress = new Uri("https://api.github.com/");

        // The GitHub API requires two headers.
        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Authorization, "token " + "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"
        );

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.Accept, "application/vnd.github.v3+json");

        _httpClient.DefaultRequestHeaders.Add(
            HeaderNames.UserAgent, "HttpRequestsSample");
    }

    public async Task<GitHubUser?> GetCurrentUser() =>
        await _httpClient.GetFromJsonAsync<GitHubUser>("/user");
}

