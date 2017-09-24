using GigHub.Core.Dtos;
using GigHub.Core.Models;
using GigHub.Persistence;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers.Api
{
    [Authorize]
    public class FollowingsController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public FollowingsController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Follow(FollowingDto dto)
        {
            var followerId = User.Identity.GetUserId();
            var IsFollowingExists = _context.Followings.Any(f => f.FollowerId == followerId && f.FolloweeId == dto.FolloweeId);

            if (IsFollowingExists)
            {
                return BadRequest("The Following alread exist");
            }

            var following = new Following
            {
                FollowerId = followerId,
                FolloweeId = dto.FolloweeId
            };

            _context.Followings.Add(following);
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var followerId = User.Identity.GetUserId();
            var following = _context.Followings.FirstOrDefault(f => f.FollowerId == followerId && f.FolloweeId == id);

            if (following == null)
            {
                return NotFound();
            }

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok(id);
        }
    }
}
