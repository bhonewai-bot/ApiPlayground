using ApiPlayground.Domain.Features.Blog;
using Microsoft.AspNetCore.Mvc;

namespace ApiPlayground.MinimalApi.Endpoints.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] IBlogService service) =>
        {
            var lts = service.GetBlogs();
            
            return Results.Ok(lts);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] IBlogService service, int id) =>
        {
            var blog = service.GetBlog(id);
            if (blog is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(blog);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] IBlogService service, TblBlog blog) =>
        {
            var result = service.CreateBlog(blog);
            
            return Results.Ok(result);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] IBlogService service, int id, TblBlog blog) =>
        {
            var result = service.UpdateBlog(id, blog);
            if (result is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(result);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", ([FromServices] IBlogService service, int id, TblBlog blog) =>
        {
            var result = service.PatchBlog(id, blog);
            if (result is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(result);
        })
        .WithName("PatchBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] IBlogService service, int id) =>
        {
            var result = service.DeleteBlog(id);
            if (result is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok();
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}