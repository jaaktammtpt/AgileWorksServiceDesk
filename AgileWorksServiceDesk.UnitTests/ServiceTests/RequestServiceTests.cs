using AgileWorksServiceDesk.Data;
using AgileWorksServiceDesk.Models;
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

        [Fact]
        public async Task GetByIdAsync_should_return_null_for_missing_request()
        {
            await InitRequestsListAsync();
            var id = -1;

            var result = await RequestServices.GetByIdAsync(id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetByIdAsync_should_return_model_for_existing_request()
        {
            await InitRequestsListAsync();
            var id = 2;

            var result = await RequestServices.GetByIdAsync(id);

            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task ExistAsync_should_return_false_for_missing_request()
        {
            await InitRequestsListAsync();
            var id = -1;

            var result = await RequestServices.ExistAsync(id);

            Assert.False(result);
        }

        [Fact]
        public async Task ExistAsync_should_return_true_for_missing_request()
        {
            await InitRequestsListAsync();
            var id = 2;

            var result = await RequestServices.ExistAsync(id);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_should_return_updated_model()
        {
            await InitRequestsListAsync();
            var id = 2;

            var request = await RequestServices.GetByIdAsync(id);

            var update = new RequestDTO
            {
                Completed = true,
                Description = "Updated desc",
                DueDateTime = request.DueDateTime
            };

            var updatedData = await RequestServices.UpdateAsync(update);

            Assert.True(updatedData.Completed);
            Assert.Equal("Updated desc", updatedData.Description);
        }

        [Fact]
        public async Task CreateAsync_should_return_created_model()
        {
            var request = new RequestDTO
            {
                Description = "New request desc",
                DueDateTime = DateTime.Now.AddHours(32)
            };

            var createdData = await RequestServices.CreateAsync(request);

            Assert.NotNull(createdData);
            Assert.Equal("New request desc", createdData.Description);
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
