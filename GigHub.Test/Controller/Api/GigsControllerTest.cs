using GigHub.Controllers.Api;
using GigHub.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Security.Claims;
using System.Security.Principal;

namespace GigHub.Test.Controller.Api
{
    [TestClass]
    public class GigsControllerTest
    {
        private readonly GigsController _controller;

        public GigsControllerTest()
        {
            var identity = new GenericIdentity("user1@domain.com");
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", "user1@domain.com"));
            identity.AddClaim(new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", "1"));

            var principal = new GenericPrincipal(identity, null);

            var mockUnitOfWork = new Mock<IUnitOfWork>();

            //mockUnitOfWork.Object means implemantation of IUnitOfwork
            _controller = new GigsController(mockUnitOfWork.Object);
            _controller.User = principal;
        }

        [TestMethod]
        public void TestMethod1()
        {
        }
    }
}
