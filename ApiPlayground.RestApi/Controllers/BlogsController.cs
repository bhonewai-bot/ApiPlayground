using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlayground.Database.AppDbContextModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiPlayground.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public BlogsController()
        {
            _db = new AppDbContext();
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lts = _db.TblBlogs
                .AsNoTracking()
                .OrderByDescending(x => x.BlogId)
                .Where(x => x.DeleteFlag == false)
                .ToList();
            
            return Ok(lts);
        }
        
        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var item = _db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id &&  x.DeleteFlag == false);
            if (item is null)
            {
                return NotFound("Blog not found");
            }
            
            return Ok(item);
        }
        
        [HttpPost()]
        public IActionResult CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            
            return Ok(blog);
        }
        
        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.DeleteFlag == false && x.BlogId == id);
            if (item is null)
            {
                return NotFound("Blog not found");
            }

            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            
            return Ok(item);
        }
        
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.DeleteFlag == false && x.BlogId == id);
            if (item is null)
            {
                return NotFound("Blog not found");
            }
            
            if (!string.IsNullOrEmpty(blog.BlogTitle))
                item.BlogTitle = blog.BlogTitle;
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
                item.BlogAuthor = blog.BlogAuthor;
            if (!string.IsNullOrEmpty(blog.BlogContent))
                item.BlogContent = blog.BlogContent;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            
            return Ok(item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var item = _db.TblBlogs
                .AsNoTracking()
                .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return NotFound("Blog not found");
            }
            
            item.DeleteFlag = true;

            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            
            return Ok();
        }
    }
}
