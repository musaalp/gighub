using FluentAssertions;
using GigHub.Controllers.Api;
using GigHub.Core;
using GigHub.Core.Models;
using GigHub.Core.Repositories;
using GigHub.Test.Controller.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Web.Http.Results;

namespace GigHub.Test.Controller.Api
{
    [TestClass]
    public class GigsControllerTest
    {
        private GigsController _controller;
        private Mock<IGigRepository> _mockRepository;
        private string _userId = string.Empty;

        [TestInitialize]
        public void TestInitialize()
        {
            //{mockVariable}.Object means implemantation of abstraction

            _mockRepository = new Mock<IGigRepository>();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.SetupGet(u => u.Gigs).Returns(_mockRepository.Object);


            _controller = new GigsController(mockUnitOfWork.Object);
            _userId = Guid.NewGuid().ToString();
            _controller.MockCurrentUser(_userId, "user1@domain.com");
        }

        [TestMethod]
        public void Cancel_NoGigWithGivenIdExists_ShouldReturnNotFound()
        {
            var result = _controller.Cancel(1);

            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_GigIsCanceled_ShouldReturnNotFound()
        {
            var gig = new Gig();
            gig.Cancel();

            _mockRepository.Setup(r => r.GetGigWithAttendees(It.IsAny<int>())).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public void Cancel_UserCancelingAnotherUserGig_SouldReturnUnauthorized()
        {
            var gig = new Gig
            {
                ArtistId = Guid.NewGuid().ToString()
            };

            _mockRepository.Setup(r => r.GetGigWithAttendees(It.IsAny<int>())).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<UnauthorizedResult>();
        }

        [TestMethod]
        public void Cancel_ValidRequest_ShouldReturnOk()
        {
            var gig = new Gig
            {
                ArtistId = _userId
            };

            _mockRepository.Setup(r => r.GetGigWithAttendees(It.IsAny<int>())).Returns(gig);

            var result = _controller.Cancel(1);
            result.Should().BeOfType<OkResult>();
        }
    }
}
