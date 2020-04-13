using AgileWorksServiceDesk.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace AgileWorksServiceDesk.UnitTests.ModelsTests
{
    
    public class RequestDTOTests
    {

        [Fact]
        public void LateIndicator_should_return_late()
        {
            var time = DateTime.Now;

            var request = new RequestDTO();
            request.Description = "New request desc";
            request.DueDateTime = time;

            Assert.Equal("late", request.LateIndicator());
        }

        [Fact]
        public void LateIndicator_should_return_notlate()
        {
            var time = DateTime.Now;

            var request = new RequestDTO();
            request.Description = "New request desc";
            request.DueDateTime = time.AddDays(1);

            Assert.Equal("notlate", request.LateIndicator());
        }


    }
}
