using System.Text;
using System.Text.Json;
using static TrollMarketWeb.Configurations.ConfigureBusinessService;

namespace TrollMarketWeb.APIGateway;

public class ConfigureAPI
{
    private readonly IConfiguration _configuration;

    public ConfigureAPI(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private HttpClient ConfiguringHttpClient(string token){
        var client = new HttpClient();
        client.BaseAddress = new Uri(_configuration.GetValue<string>("API:BaseUrl"));
        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        return client;
    }

    public async Task<HttpResponseMessage> GetAPI(string url, string token){
        var client = ConfiguringHttpClient(token);
        return await client.GetAsync(url);
    }

    public async Task<string> PostAPI<T>(T viewModel, string token, string url){
        var client = ConfiguringHttpClient(token);
        var response = await client.PostAsJsonAsync(url, viewModel);
        var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : await response.Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> PutAPI<T>(T viewModel, string token, string url){
        var client = ConfiguringHttpClient(token);
        var response = await client.PutAsJsonAsync(url, viewModel);
        var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : await response.Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> PatchAPI<T>(T viewModel, string token, string url){
        var client = ConfiguringHttpClient(token);
        var stringContent = new StringContent(JsonSerializer.Serialize(viewModel), Encoding.UTF8, "application/json");
        var response = await client.PatchAsync(url, stringContent);
        var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : await response.Content.ReadAsStringAsync();
        return content;
    }

    public async Task<string> DeleteAPI(string token, string url){
        var client = ConfiguringHttpClient(token);
        var response = await client.DeleteAsync(url);
        var content = response.IsSuccessStatusCode ? await response.Content.ReadAsStringAsync() : await response.Content.ReadAsStringAsync();
        return content;
    }
}
