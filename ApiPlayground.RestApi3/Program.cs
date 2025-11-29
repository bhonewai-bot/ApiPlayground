using Refit;
using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton(n => new HttpClient()
{
    BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!)
});

builder.Services.AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));    

builder.Services
    .AddRefitClient<ISnakeApi>()
    .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("ApiDomainUrl").Value!));

// builder.Services.AddSingleton(n => new RestClient(builder.Configuration.GetSection("ApiDomainUrl").Value!));

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

app.UseAuthorization();

app.MapControllers();

app.Run();