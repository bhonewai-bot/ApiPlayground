using Refit;

namespace ApiPlayground.ConsoleApp2;

public interface IBlogApi
{
    [Get("/api/blogs")]
    Task<List<BlogModel>> GetBlogs();
}

public class BlogModel
{
    public int BlogId { get; set; }
    
    public string BlogTitle { get; set; }
    
    public string BlogAuthor { get; set; }
    
    public string BlogContent { get; set; }
}