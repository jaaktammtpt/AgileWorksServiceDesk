using AgileWorksServiceDesk.Helpers;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using System;
using Xunit;

namespace AgileWorksServiceDesk.UnitTests
{
    public abstract class TestBase
    {
        protected IMapper Mapper { get; private set; }
        public TestBase()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            Mapper = config.CreateMapper();
        }
        public ILogger<T> GetLogget<T>()
        {
            return NullLogger<T>.Instance;
        }
    }
}
