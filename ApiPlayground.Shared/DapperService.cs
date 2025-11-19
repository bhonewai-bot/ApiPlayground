using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace ApiPlayground.Shared;

public class DapperService
{
    private readonly string _connectionString;

    public DapperService(string connectionString)
    {
        _connectionString = connectionString;
    }

    public List<T> Query<T>(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var lts = db.Query<T>(query, param).ToList();
        
        return lts;
    }

    public T QuerySingle<T>(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        var item = db.QueryFirstOrDefault<T>(query, param);
        
        return item;
    }

    public int Execute(string query, object? param = null)
    {
        using IDbConnection db = new SqlConnection(_connectionString);
        int result = db.Execute(query, param);
        return result;
    }
}