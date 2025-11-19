using System.Text.Json.Serialization;
using Newtonsoft.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/birds", () =>
    {
        string folderPath = "Data/Birds.json";
        var jsonStr = File.ReadAllText(folderPath);
        var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
        
        return Results.Ok(result.Tbl_Bird); 
    })
.WithName("GetBirds")
.WithOpenApi();

app.MapGet("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    var bird = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);

    if (bird is null)
    {
        return Results.NotFound("Bird not found");
    }
    
    return Results.Ok(bird); 
})
.WithName("GetBird")
.WithOpenApi();

app.MapPost("/birds", (BirdModel requestModel) =>
    {
        string folderPath = "Data/Birds.json";
        string jsonStr = File.ReadAllText(folderPath);
        var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

        requestModel.Id = result.Tbl_Bird.Count == 0 ? 1 : result.Tbl_Bird.Max(x => x.Id) + 1;
        result.Tbl_Bird.Add(requestModel);

        string jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
        File.WriteAllText(folderPath, jsonStrToWrite);
        
        return Results.Ok(requestModel);
    })
.WithName("CreateBird")
.WithOpenApi();

app.MapPut("/birds/{id}", (int id, BirdModel requestModel) =>
{
    string folderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;

    var bird = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (bird is null)
    {
        return Results.NotFound("Bird not found");
    }

    bird.BirdMyanmarName = requestModel.BirdMyanmarName;
    bird.BirdEnglishName = requestModel.BirdEnglishName;
    bird.Description = requestModel.Description;
    bird.ImagePath = requestModel.ImagePath;

    string jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, jsonStrToWrite);

    return Results.Ok(bird);
})
.WithName("UpdateBird")
.WithOpenApi();

app.MapDelete("/birds/{id}", (int id) =>
{
    string folderPath = "Data/Birds.json";
    string jsonStr = File.ReadAllText(folderPath);
    var result = JsonConvert.DeserializeObject<BirdResponseModel>(jsonStr)!;
    
    var bird = result.Tbl_Bird.FirstOrDefault(x => x.Id == id);
    if (bird is null)
    {
        return Results.NotFound("Bird not found");
    }

    result.Tbl_Bird.Remove(bird);

    string jsonStrToWrite = JsonConvert.SerializeObject(result, Formatting.Indented);
    File.WriteAllText(folderPath, jsonStrToWrite);
    
    return Results.Ok("Deleted Bird");
})
.WithName("DeleteBird")
.WithOpenApi();

app.Run();

public class BirdResponseModel
{
    public List<BirdModel> Tbl_Bird { get; set; }
}

public class BirdModel
{
    public int Id { get; set; }
    public string BirdMyanmarName { get; set; }
    public string BirdEnglishName { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}

