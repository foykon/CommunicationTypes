using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await Task.Delay(15000); 
            return Ok(new string[] { "Furkan", "YILDIZ" });
        }
    }
}
