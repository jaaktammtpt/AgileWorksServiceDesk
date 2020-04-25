using Microsoft.AspNetCore.Mvc.Testing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace AgileWorksServiceDesc.IntegrationTests
{
    public class TestBase : IClassFixture<TestApplicationFactory<FakeStartup>>
    {
        protected WebApplicationFactory<FakeStartup> Factory { get; }

        public TestBase(TestApplicationFactory<FakeStartup> factory)
        {
            Factory = factory;
        }
    }
}
