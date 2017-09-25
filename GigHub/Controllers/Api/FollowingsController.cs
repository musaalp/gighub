using GigHub.Core;
using GigHub.Core.Dtos;
using GigHub.Core.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FollowingsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var followerId = User.Identity.GetUserId();
            var following = _unitOfWork.Followings.GetFollowing(dto.FolloweeId, followerId);

            if (following != null)
            {
                return BadRequest("The Following alread exist");
            }

            following = new Following
            {
                FollowerId = followerId,
                FolloweeId = dto.FolloweeId
            };
                        
            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var followerId = User.Identity.GetUserId();

            var following = _unitOfWork.Followings.GetFollowing(id, followerId);

            if (following == null)
            {
                return NotFound();
            }
                        
            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complete();

            return Ok(id);
        }
    }
}
