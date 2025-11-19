// See https://aka.ms/new-console-template for more information

using Newtonsoft.Json;

var blog = new BlogModel()
{
    Id = 1,
    Title = "Blog 1",
    Author = "Author 1",
    Content = "Blog 1",
};

/*string jsonStr = JsonConvert.SerializeObject(blog, Formatting.Indented);
Console.WriteLine(jsonStr);*/
string jsonStr = blog.ToJson();
Console.WriteLine(jsonStr);

string jsonStr2 = """{"Id":1,"Title":"Blog 1","Author":"Author 1","Content":"Blog 1"}""";
var blog2 = JsonConvert.DeserializeObject<BlogModel>(jsonStr2);
Console.WriteLine(blog2?.Title);

public class BlogModel
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Author { get; set; }
    
    public string Content { get; set; }
}

public static class Extensions
{
    public static string ToJson(this object obj)
    {
        string jsonStr = JsonConvert.SerializeObject(obj, Formatting.Indented);
        return jsonStr;
    }
}