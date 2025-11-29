using Microsoft.AspNetCore.Mvc;

namespace ApiPlayground.MinimalApi.Endpoints.Blog;

public static class BlogEndpoint
{
    public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            var response = db.TblBlogs
                .Where(x => x.DeleteFlag == false)
                .OrderByDescending(x => x.BlogId)
                .AsNoTracking()
                .ToList();
            return Results.Ok(response);
        })
        .WithName("GetBlogs")
        .WithOpenApi();

        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var response = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

            if (response is null)
            {
                return Results.NotFound("Blog not found");
            }

            return Results.Ok(response);
        })
        .WithName("GetBlog")
        .WithOpenApi();

        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
        {
            db.TblBlogs.Add(blog);
            db.SaveChanges();
            
            return Results.Ok(blog);
        })
        .WithName("CreateBlog")
        .WithOpenApi();

        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            var item = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
            if (item is null)
            {
                return Results.NotFound("Blog not found");
            }
            
            item.BlogId = id;
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            item.DeleteFlag = false;

            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();

            return Results.Ok(item);
        })
        .WithName("UpdateBlog")
        .WithOpenApi();

        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            var item = db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

            if (item is null)
            {
                return Results.NotFound("Blog not found");
            }

            item.DeleteFlag = true;

            db.Entry(item).State = EntityState.Modified;
            int result = db.SaveChanges();

            return Results.Ok(result > 0 ? "Deleting successfully" : "Deleting failed");
        })
        .WithName("DeleteBlog")
        .WithOpenApi();
    }
}