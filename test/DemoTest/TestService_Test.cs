using JenkinsDemo;
using System;
using Xunit;

namespace DemoTest
{
    public class TestService_Test
    {
        [Fact]
        public void AddTest()
        {
            ITestService service = new TestService();
            var value = service.Add(6,66);
            Assert.Equal(72,value);
        }
    }
}
