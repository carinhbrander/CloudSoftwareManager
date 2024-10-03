using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
[Authorize]
public class SoftwareController(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet]
    public IEnumerable<SoftwareContract> Get()
    {
        var softwareHandler = new SoftwareHandler(_context);
        return softwareHandler.GetSoftwareList();
    }
}