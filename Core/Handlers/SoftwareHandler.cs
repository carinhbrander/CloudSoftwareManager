public class SoftwareHandler(ApplicationDbContext context)
{
    private readonly ApplicationDbContext _context = context;
    public IEnumerable<SoftwareContract> GetSoftwareList()
    {
        var api = new CCPApi();
        return api.ListSoftware();
    }
}