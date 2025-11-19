using ApiPlayground.ConsoleApp.Models;
using ApiPlayground.Shared;

namespace ApiPlayground.ConsoleApp;

public class DapperExample
{
    private readonly string _connectionString = "Server=.;Database=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
    private readonly DapperService _dapperService;

    public DapperExample()
    {
        _dapperService = new DapperService(_connectionString);
    }

    public void Read()
    {
        string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";

        var lts = _dapperService.Query<BlogDataModel.BlogDapperModel>(query);
        foreach (var item in lts)
        {
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
    }

    public void Edit(int id)
    {
        string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId AND DeleteFlag = 0";

        var item = _dapperService.QuerySingle<BlogDataModel.BlogDapperModel>(query, new { BlogId = id });
        if (item is null)
        {
            Console.WriteLine("Blog not found");
            return;
        }
        
        Console.WriteLine(item.BlogId);
        Console.WriteLine(item.BlogTitle);
        Console.WriteLine(item.BlogAuthor);
        Console.WriteLine(item.BlogContent);
    }

    public void Create(string title, string author, string content)
    {
        string query = @"
            INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent)
            VALUES (@BlogTitle, @BlogAuthor, @BlogContent);";

        int result = _dapperService.Execute(query, new BlogDataModel.BlogDapperModel()
        {
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        });

        Console.WriteLine(result > 0 ? "Saving successfully" : "Saving failed");
    }

    public void Update(int id, string title, string author, string content)
    {
        string query = @"
            UPDATE Tbl_Blog SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent
            WHERE BlogId = @BlogId";
        
        int result = _dapperService.Execute(query, new BlogDataModel.BlogDapperModel()
        {
            BlogId = id,
            BlogTitle = title,
            BlogAuthor = author,
            BlogContent = content
        });

        Console.WriteLine(result > 0 ? "Updating successfully" : "Updating failed");
    }

    public void Delete(int id)
    {
        string query = "UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";
        
        int result = _dapperService.Execute(query, new BlogDataModel.BlogDapperModel()
        {
            BlogId = id
        });

        Console.WriteLine(result > 0 ? "Deleting successfully" : "Deleting failed");
    }
}