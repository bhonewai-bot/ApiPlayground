using System.Data;
using ApiPlayground.Shared;

namespace ApiPlayground.ConsoleApp;

public class AdoDotNetExample
{
    private readonly string _connectionString = "Server=.;Database=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";
    private readonly AdoDotNetService _adoDotNetService;

    public AdoDotNetExample()
    {
        _adoDotNetService = new AdoDotNetService(_connectionString);
    }

    public void Read()
    {
        string query = "SELECT * FROM Tbl_Blog WHERE DeleteFlag = 0";
        var dt = _adoDotNetService.Query(query);

        foreach (DataRow dr in dt.Rows)
        {
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
        }
    }

    public void Edit(int id)
    {
        string query = "SELECT * FROM Tbl_Blog WHERE BlogId = @BlogId AND DeleteFlag = 0";

        var dt = _adoDotNetService.Query(query, new SqlParameterModel("@BlogId", id));
        
        DataRow dr = dt.Rows[0];
        Console.WriteLine(dr["BlogId"]);
        Console.WriteLine(dr["BlogTitle"]);
        Console.WriteLine(dr["BlogAuthor"]);
        Console.WriteLine(dr["BlogContent"]);
    }

    public void Create(string title, string author, string content)
    {
        string query = @"
            INSERT INTO Tbl_Blog (BlogTitle, BlogAuthor, BlogContent)
            VALUES (@BlogTitle, @BlogAuthor, @BlogContent);";

        int result = _adoDotNetService.Execute(query, 
            new SqlParameterModel("@BlogTitle", title),
            new SqlParameterModel("@BlogAuthor", author),
            new SqlParameterModel("@BlogContent", content));

        Console.WriteLine(result > 0 ? "Saving successfully" : "Saving failed");
    }

    public void Update(int id, string title, string author, string content)
    {
        string query = @"
            UPDATE Tbl_Blog SET BlogTitle = @BlogTitle, BlogAuthor = @BlogAuthor, BlogContent = @BlogContent
            WHERE BlogId = @BlogId";
        
        int result = _adoDotNetService.Execute(query, 
            new SqlParameterModel("@BlogId", id),
            new SqlParameterModel("@BlogTitle", title),
            new SqlParameterModel("@BlogAuthor", author),
            new SqlParameterModel("@BlogContent", content));
        
        Console.WriteLine(result > 0 ? "Updating successfully" : "Updating failed");
    }

    public void Delete(int id)
    {
        string query = "UPDATE Tbl_Blog SET DeleteFlag = 1 WHERE BlogId = @BlogId";

        int result = _adoDotNetService.Execute(query, new SqlParameterModel("@BlogId", id));
        
        Console.WriteLine(result > 0 ? "Deleting successfully" : "Deleting failed");
    }
}