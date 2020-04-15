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
        public async Task Create_InvalidData_should_return_NotOkResult()
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
        public async Task Create_ValidData_return_OkResult()
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
        public void Create_should_return_create_view()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(logger.Object, requestServiceMock.Object);

            var result = controller.Create() as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Create");
        }

        [Fact]
        public async Task Edit_should_return_BadRequest()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(logger.Object, requestServiceMock.Object);
            var id = (int?)null;

            var result = await controller.Edit(id);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_NotFound()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();
            requestServiceMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => null);

            var controller = new HomeController(logger.Object, requestServiceMock.Object);

            var id = -1;

            var result = await controller.Edit(id);

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Edit_should_return_view_with_existing_request()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();
            var request = new RequestDTO();
            requestServiceMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => request);
            var controller = new HomeController(logger.Object, requestServiceMock.Object);
            var id = 5;

            var result = await controller.Edit(id) as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Edit");
            Assert.Equal(request, result.Model);
        }

        [Fact]
        public async Task Edit_should_return_OkResult()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();
            
            var request = new RequestDTO();
            requestServiceMock.Setup(c => c.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(() => request);
            var controller = new HomeController(logger.Object, requestServiceMock.Object);
            //var id = request.Id;

            var data = await controller.Edit(request);
            Assert.IsType<RedirectToActionResult>(data);
        }

        [Fact]
        public async Task Edit_InvalidData_should_return_edit_view()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();

            var controller = new HomeController(logger.Object, requestServiceMock.Object);
            controller.ModelState.AddModelError("Description", "Required");

            var request = new RequestDTO();

            var result = await controller.Edit(request);

            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<RequestDTO>(viewResult.ViewData.Model);
        }

        [Fact]
        public void Privacy_should_return_privacy_view()
        {
            var requestServiceMock = new Mock<IRequestService>();
            var logger = new Mock<ILogger<HomeController>>();
            var controller = new HomeController(logger.Object, requestServiceMock.Object);

            var result = controller.Privacy() as ViewResult;
            var viewName = result.ViewName;

            Assert.True(string.IsNullOrEmpty(viewName) || viewName == "Privacy");
        }

    }
}
