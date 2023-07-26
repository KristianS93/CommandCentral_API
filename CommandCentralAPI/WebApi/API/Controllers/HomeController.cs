using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class HomeController : ControllerBase
{
    [HttpGet]
    [Route("/")]
    public IActionResult IndexPage()
    {
        string htmlContent = "<html><body><h1>Welcome to the home page</h1><a href=https://github.com/KristianS93/CommandCentral_API>Repo Link</a></body></html>";
        return Content(htmlContent, "text/html");
    }
}