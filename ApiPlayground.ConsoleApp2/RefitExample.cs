using Refit;

namespace ApiPlayground.ConsoleApp2;

public class RefitExample
{
    public async Task RunAsync()
    {
        var blogApi = RestService.For<IBlogApi>("https://localhost:7047");
        var lts = await blogApi.GetBlogs();

        foreach (var item in lts)
        {
            Console.WriteLine(item.BlogContent);
        }
    }
}