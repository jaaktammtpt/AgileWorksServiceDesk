using AgileWorksServiceDesk.Controllers;
using AgileWorksServiceDesk.Models;
using AgileWorksServiceDesk.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AgileWorksServiceDesk.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        [Fact]
        public async Task Index_should_return_index_or_default_view()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();

            requestServiceMock.Setup(c => c.GetAllActiveRequests())
                .ReturnsAsync(() => new List<RequestDTO>());

            var controller = new HomeController(logger.Object, requestServiceMock.Object);

            var result = await controller.Index() as ViewResult;

            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Index");
        }

        [Fact]
        public async Task Create_ValidData_Return_NotOkResult()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();
            
            var controller = new HomeController(logger.Object, requestServiceMock.Object);
            controller.ModelState.AddModelError("Description", "Required");

            var request = new RequestDTO();
            

            var result = await controller.Create(request);


            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<RequestDTO>(viewResult.ViewData.Model);
        }

        [Fact]
        public async Task Create_ValidData_Return_OkResult()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(logger.Object, requestServiceMock.Object);

            var request = new RequestDTO
            {
                Description = "New request desc",
                DueDateTime = DateTime.Now.AddHours(32)
            };

            var data = await controller.Create(request);
            Assert.IsType<RedirectToActionResult>(data);
        }

        [Fact]
        public void Create_return_view()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(logger.Object, requestServiceMock.Object);

            var result = controller.Create() as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Create");
        }
    }
}
