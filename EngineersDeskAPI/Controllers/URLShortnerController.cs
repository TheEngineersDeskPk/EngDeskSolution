using Microsoft.AspNetCore.Mvc;

namespace EngineersDeskAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class URLShortnerController : Controller
    {  
        [HttpGet]
        public IActionResult ShortneURL(string longURL)
        {
            if(string.IsNullOrEmpty(longURL))
            {
                return BadRequest("Please provide a valid URL");
            }
            try
            {
                System.Uri address = new System.Uri("http://tinyurl.com/api-create.php?url=" + longURL);
                System.Net.WebClient client = new System.Net.WebClient();
                string tinyURL = client.DownloadString(address);
                return Ok(tinyURL);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
            }
        }
    }
}
