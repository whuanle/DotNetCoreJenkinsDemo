using JenkinsDemo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebDemo.Controllers
{
    [Route("/")]
    public class DefaultConroller : ControllerBase
    {
        private readonly ITestService _service;
        public DefaultConroller(ITestService service)
        {
            _service = service;
        }

        [Route("/")]
        [HttpGet]
        public string Hello()
        {
            return "正在访问 web";
        }
    }
}
