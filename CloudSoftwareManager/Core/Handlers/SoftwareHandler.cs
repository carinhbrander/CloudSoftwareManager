public class SoftwareHandler()
{
    public async Task<List<SoftwareContract>> GetSoftwareList()
    {
        var api = new CCPApi();
        return await api.ListSoftware();
    }
}