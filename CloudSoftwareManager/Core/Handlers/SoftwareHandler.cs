public class SoftwareHandler()
{
    public IEnumerable<SoftwareContract> GetSoftwareList()
    {
        var api = new CCPApi();
        return api.ListSoftware();
    }
}