using AgileWorksServiceDesk.Data;
using AgileWorksServiceDesk.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgileWorksServiceDesk.UnitTests.ServiceTests
{
    public class RequestServiceTests : ServiceTestBase
    {
        private readonly RequestService RequestServices;

        public RequestServiceTests()
        {
            RequestServices = new RequestService(DbContext, Mapper);
        }

        [Fact]
        public async Task GetAllActiveRequests_should_return_list_of_requests()
        {
            await InitRequestsListAsync();

            var result = await RequestServices.GetAllActiveRequests();

            Assert.NotNull(result);
            Assert.Equal(9, result.Count());
        }

        private async Task InitRequestsListAsync()
        {
            for (int i = 0; i < 9; i++)
            {
                DbContext.Requests.Add(new Request
                {
                    Description = "Request no " + i,
                    DueDateTime = DateTime.Now.AddHours(-5 + i)
                });
            }

            DbContext.Requests.Add(new Request
            {
                Description = "Request no " + 9,
                DueDateTime = DateTime.Now.AddHours(-5 + 9),
                Completed = true
            });

            await DbContext.SaveChangesAsync();
        }
    }
}
