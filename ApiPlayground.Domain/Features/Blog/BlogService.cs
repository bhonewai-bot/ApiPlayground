using ApiPlayground.Database.AppDbContextModels;
using ApiPlayground.Domain.Models;
using ApiPlayground.Domain.Models.Blog;
using Microsoft.EntityFrameworkCore;

namespace ApiPlayground.Domain.Features.Blog;

public class BlogService
{
    private readonly AppDbContext _db = new AppDbContext();

    public List<TblBlog> GetBlogs()
    {
        var model = _db.TblBlogs
            .Where(x => x.DeleteFlag == false)
            .AsNoTracking().ToList();
        return model;
    }

    public TblBlog GetBlog(int id)
    {
        var model = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);
        
        return model;
    }

    public TblBlog CreateBlog(TblBlog blog)
    {
        _db.TblBlogs.Add(blog);
        _db.SaveChanges();
        
        return blog;
    }

    public TblBlog UpdateBlog(int id, TblBlog blog)
    {
        var model = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

        if (model is null)
        {
            return null;
        }
        
        model.BlogTitle = blog.BlogTitle;
        model.BlogAuthor = blog.BlogAuthor;
        model.BlogContent = blog.BlogContent;

        _db.Entry(model).State = EntityState.Modified;
        _db.SaveChanges();
        
        return model;
    }

    public TblBlog PatchBlog(int id, TblBlog blog)
    {
        var model = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

        if (model is null)
        {
            return null;
        }

        if (!string.IsNullOrEmpty(model.BlogTitle))
            model.BlogTitle = blog.BlogTitle;
        if (!string.IsNullOrEmpty(model.BlogAuthor))
            model.BlogAuthor = blog.BlogAuthor;
        if (!string.IsNullOrEmpty(model.BlogContent))
            model.BlogContent = blog.BlogContent;

        _db.Entry(model).State = EntityState.Modified;
        _db.SaveChanges();
        
        return model;
    }

    public bool? DeleteBlog(int id)
    {
        var model = _db.TblBlogs
            .AsNoTracking()
            .FirstOrDefault(x => x.BlogId == id && x.DeleteFlag == false);

        if (model is null)
        {
            return false;
        }

        _db.Entry(model).State = EntityState.Deleted;
        int result = _db.SaveChanges();
        
        return result > 0;
    }
}