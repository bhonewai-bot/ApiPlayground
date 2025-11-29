using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiPlayground.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /*public async Task<IActionResult> Test()
        {
            HttpClient client = new HttpClient();
            return await client.GetAsync("birds");
        }*/
    }
}
