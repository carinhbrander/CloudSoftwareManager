using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SoftwareController : ControllerBase
{
    [HttpGet]
    public IEnumerable<SoftwareContract> Get()
    {
        var softwareHandler = new SoftwareHandler();
        return softwareHandler.GetSoftwareList();
    }
}