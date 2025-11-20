using ApiPlayground.Domain.Features.Blog;

namespace ApiPlayground.MinimalApi.Endpoints.Blog;

public static class BlogServiceEndpoint
{
    public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", () =>
        {
            BlogService service = new BlogService();
            var lts = service.GetBlogs();
            
            return Results.Ok(lts);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();
            var blog = service.GetBlog(id);
            if (blog is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(blog);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", (TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var result = service.CreateBlog(blog);
            
            return Results.Ok(result);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", (int id, TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var result = service.UpdateBlog(id, blog);
            if (result is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(result);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapPatch("/blogs/{id}", (int id, TblBlog blog) =>
        {
            BlogService service = new BlogService();
            var result = service.PatchBlog(id, blog);
            if (result is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(result);
        })
        .WithName("PatchBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", (int id) =>
        {
            BlogService service = new BlogService();

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