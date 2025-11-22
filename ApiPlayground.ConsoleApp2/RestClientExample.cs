using System.Net;
using RestSharp;

namespace ApiPlayground.ConsoleApp2;

public class RestClientExample
{
    private readonly RestClient _client;
    private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";

    public RestClientExample()
    {
        _client = new RestClient();
    }

    public async Task Read()
    {
        RestRequest request = new RestRequest(_postEndpoint, Method.Get);
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }
    }
    
    public async Task Edit(int id)
    {
        RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Get);
        var response = await _client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data Found");
            return;
        }
        
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }
    }
    
    public async Task Create(int userId, string title, string body)
    {
        PostModel requestModel = new PostModel()
        {
            userId = userId,
            title = title,
            body = body
        };
        
        RestRequest request = new RestRequest(_postEndpoint, Method.Post);
        request.AddJsonBody(requestModel);
        
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content!);
        }
    }
    
    public async Task Update(int id, int userId, string title, string body)
    {
        PostModel requestModel = new PostModel()
        {
            id = id,
            userId = userId,
            title = title,
            body = body
        };
        
        RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Patch);
        request.AddJsonBody(requestModel);
        
        var response = await _client.ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
        {
            Console.WriteLine(response.Content!);
        }
    }
    
    public async Task Delete(int id)
    {
        RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Delete);
        var response = await _client.ExecuteAsync(request);
        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            Console.WriteLine("No data Found");
            return;
        }
        
        if (response.IsSuccessStatusCode)
        {
            string jsonStr = response.Content!;
            Console.WriteLine(jsonStr);
        }
    }
}