using System;
using System.Collections.Generic;
using System.Text;

namespace JenkinsDemo
{
    public class TestService : ITestService
    {
        public int Add(int x, int y)
        {
            return x + y;
        }
    }
}
