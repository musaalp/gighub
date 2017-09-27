using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Repositories;
using GigHub.Test.Controller.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Http.Results;

namespace GigHub.Test.Controller.Api
{
    [TestClass]
    public class GigsControllerTest
    {
        private readonly GigsController _controller;

        public GigsControllerTest()
        {
            //{mockVariable}.Object means implemantation of abstraction

            var mockRepository = new Mock<IGigRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(mockRepository.Object);

            
            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.MockCurrentUser("1", "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);
            
            result.Should().BeOfType<NotFoundResult>();
        }       
    }
}
