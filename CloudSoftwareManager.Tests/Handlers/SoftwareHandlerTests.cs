namespace Tests
{
    public class SoftwareHandlerTests
    {
        [Fact]
        public void GetSoftwareList()
        {
            var handler = new SoftwareHandler();
            var list = handler.GetSoftwareList();
            Assert.NotNull(list);
        }
    }
}