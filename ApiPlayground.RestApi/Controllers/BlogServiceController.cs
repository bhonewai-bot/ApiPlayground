using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiPlayground.Database.AppDbContextModels;
using ApiPlayground.Domain.Features.Blog;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPlayground.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogServiceController : ControllerBase
    {
        private readonly IBlogService _service;

        public BlogServiceController(IBlogService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetBlogs()
        {
            var lts = _service.GetBlogs();
            return Ok(lts);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {
            var blog = _service.GetBlog(id);
            if (blog is null)
            {
                return NotFound("Blog not found");
            }

            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(TblBlog blog)
        {
            var result = _service.CreateBlog(blog);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, TblBlog blog)
        {
            var result = _service.UpdateBlog(id, blog);
            if (result is null)
            {
                return NotFound("Blog not found");
            }
            
            return Ok(result);
        }
        
        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, TblBlog blog)
        {
            var result = _service.PatchBlog(id, blog);
            if (result is null)
            {
                return NotFound("Blog not found");
            }
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            var result = _service.DeleteBlog(id);
            if (result is null)
            {
                return NotFound("Blog not found");
            }
            
            return Ok();
        }
    }
}
